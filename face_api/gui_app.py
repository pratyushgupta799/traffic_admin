"""
gui_app.py — TrafficAdmin v6
Fixed layout: camera fills left panel properly, buttons at bottom, form on right.
"""
import tkinter as tk
from tkinter import filedialog
from tkinter import messagebox
import requests, threading, cv2, io
from PIL import Image, ImageTk
from datetime import datetime

API  = "http://localhost:5000"
NAVY = "#12477F"
GOLD = "#D49D13"
WHITE= "#FFFFFF"
LIGHT= "#F2F4F7"
RED  = "#C0392B"
GREEN= "#1E8449"
GRAY = "#95A5A6"
DARK = "#2C3E50"

CAM_W = 760
CAM_H = 500

class App:
    def __init__(self, root):
        self.root       = root
        self.root.title("TrafficAdmin — Violation Recording")
        self.root.geometry("1100x680")
        self.root.configure(bg=DARK)
        self.root.resizable(False, False)
        self.cap        = None
        self._running   = False
        self._clf_job   = None
        self._captured  = None
        self._last_cat  = "General"
        self._last_fine = 500
        self._build()
        self._ping()
        self.root.protocol("WM_DELETE_WINDOW", self._on_close)

    def _build(self):
        # ── Header ────────────────────────────────────────────
        hdr = tk.Frame(self.root, bg=NAVY, height=50)
        hdr.pack(fill="x")
        hdr.pack_propagate(False)
        tk.Label(hdr, text="🚦  TrafficAdmin — Violation Recording",
                 font=("Segoe UI",14,"bold"),
                 bg=NAVY, fg=WHITE).pack(side="left", padx=18, pady=10)
        self.api_lbl = tk.Label(hdr, text="⬤ Checking...",
                                font=("Segoe UI",9), bg=NAVY, fg=GOLD)
        self.api_lbl.pack(side="right", padx=18)
        tk.Frame(self.root, bg=GOLD, height=3).pack(fill="x")

        # ── Body row ──────────────────────────────────────────
        body = tk.Frame(self.root, bg=DARK)
        body.pack(fill="both", expand=True)

        # ══ LEFT PANEL — fixed size ═══════════════════════════
        left = tk.Frame(body, bg="#111111", width=CAM_W)
        left.pack(side="left", fill="y")
        left.pack_propagate(False)

        # Camera display — fixed size
        self.cam_lbl = tk.Label(left, bg="#111111",
                                width=CAM_W, height=CAM_H,
                                text="📷\n\nCamera is off\n\nClick  ▶ Start Camera",
                                font=("Segoe UI",14), fg="#555555")
        self.cam_lbl.pack_propagate(False)
        self.cam_lbl.place(x=0, y=0, width=CAM_W, height=CAM_H)

        # Button bar — fixed at bottom of left panel
        btn_bar = tk.Frame(left, bg="#1A1A1A", height=80)
        btn_bar.place(x=0, y=CAM_H, width=CAM_W, height=80)
        btn_bar.pack_propagate(False)

        self.cam_btn = tk.Button(btn_bar,
                                 text="▶  Start Camera",
                                 font=("Segoe UI",11,"bold"),
                                 bg=GREEN, fg=WHITE,
                                 activebackground="#145A32",
                                 relief="flat", cursor="hand2",
                                 padx=24, pady=14,
                                 command=self._toggle_camera)
        self.cam_btn.place(x=16, y=12)

        self.cap_btn = tk.Button(btn_bar,
                                 text="📸  Capture & Identify",
                                 font=("Segoe UI",11,"bold"),
                                 bg="#333333", fg="#777777",
                                 activebackground="#B7950B",
                                 relief="flat", cursor="hand2",
                                 padx=24, pady=14,
                                 state="disabled",
                                 command=self._capture)
        self.cap_btn.place(x=210, y=12)

        self.cap_status = tk.Label(btn_bar, text="",
                                   font=("Segoe UI",11,"bold"),
                                   bg="#1A1A1A", fg="#2ECC71")
        self.cap_status.place(x=460, y=22)

        self.upload_btn = tk.Button(
            btn_bar,
            text="Upload Image",
            font=("Segoe UI", 11, "bold"),
            bg="#5DADE2", fg=WHITE,
            activebackground="#2E86C1",
            relief="flat", cursor="hand2",
            padx=24, pady=14,
            command=self._upload_image
        )
        self.upload_btn.place(x=460, y = 12)

        # ══ RIGHT PANEL — form ════════════════════════════════
        right = tk.Frame(body, bg=WHITE)
        right.pack(side="right", fill="both", expand=True)

        tk.Label(right, text="Violation Details",
                 font=("Segoe UI",12,"bold"),
                 bg=WHITE, fg=NAVY).pack(anchor="w", padx=18, pady=(16,4))
        tk.Frame(right, bg=GOLD, height=2).pack(fill="x", padx=18)

        frm = tk.Frame(right, bg=WHITE)
        frm.pack(fill="both", expand=True, padx=18, pady=10)

        def lbl(t):
            tk.Label(frm, text=t, font=("Segoe UI",9,"bold"),
                     bg=WHITE, fg=DARK).pack(anchor="w", pady=(8,2))

        def ent(var):
            e = tk.Entry(frm, textvariable=var, font=("Segoe UI",10),
                         relief="flat", bg=LIGHT,
                         highlightbackground="#CCD1D9", highlightthickness=1)
            e.pack(fill="x", ipady=6)
            return e

        self.place_v = tk.StringVar()
        self.city_v  = tk.StringVar()
        self.time_v  = tk.StringVar(value=datetime.now().strftime("%d-%b-%Y %H:%M"))

        lbl("Place / Location");  ent(self.place_v)
        lbl("City");              ent(self.city_v)
        lbl("Date & Time");       ent(self.time_v)

        lbl("Violation  (describe freely — AI classifies)")
        self.vio_text = tk.Text(frm, font=("Segoe UI",10), height=4,
                                relief="flat", bg=LIGHT,
                                highlightbackground="#CCD1D9",
                                highlightthickness=1, wrap="word")
        self.vio_text.pack(fill="x", pady=(0,6))
        self.vio_text.bind("<KeyRelease>", self._on_type)

        # Classification badge
        bdg = tk.Frame(frm, bg="#EBF5FB", pady=5)
        bdg.pack(fill="x", pady=(0,12))
        tk.Label(bdg, text="  Classified as:",
                 font=("Segoe UI",8), bg="#EBF5FB", fg=GRAY).pack(side="left")
        self.cat_lbl = tk.Label(bdg, text="—",
                                font=("Segoe UI",9,"bold"),
                                bg="#EBF5FB", fg=NAVY)
        self.cat_lbl.pack(side="left", padx=4)
        self.fine_lbl = tk.Label(bdg, text="",
                                 font=("Segoe UI",9,"bold"),
                                 bg="#EBF5FB", fg=RED)
        self.fine_lbl.pack(side="right", padx=8)

        self.sub_btn = tk.Button(frm, text="🔍  Submit Violation",
                                 font=("Segoe UI",11,"bold"),
                                 bg=GOLD, fg=WHITE, relief="flat",
                                 activebackground="#B7950B",
                                 cursor="hand2", padx=16, pady=11,
                                 state="disabled",
                                 command=self._submit)
        self.sub_btn.pack(fill="x")

        # Status bar
        self.status = tk.Label(self.root,
                               text="  Start camera → hold face up → Capture → fill details → Submit",
                               font=("Segoe UI",9),
                               bg=NAVY, fg=WHITE, anchor="w")
        self.status.pack(fill="x", side="bottom")

    # ── Camera ─────────────────────────────────────────────────
    def _toggle_camera(self):
        if self._running: self._stop_camera()
        else:             self._start_camera()

    def _start_camera(self):
        self.cap = cv2.VideoCapture(0)
        if not self.cap.isOpened():
            messagebox.showerror("Camera Error",
                "Cannot open camera. Check your webcam is connected.")
            return
        self._running = True
        self.cam_btn.config(text="⏹  Stop Camera",
                            bg=RED, activebackground="#922B21")
        self.cap_btn.config(state="normal",
                            bg=GOLD, fg=WHITE,
                            activebackground="#B7950B")
        self._st("Camera live — hold the face up to the camera, then click Capture")
        self._feed()

    def _stop_camera(self):
        self._running = False
        if self.cap: self.cap.release(); self.cap = None
        self.cam_btn.config(text="▶  Start Camera",
                            bg=GREEN, activebackground="#145A32")
        self.cap_btn.config(state="disabled",
                            bg="#333333", fg="#777777")
        self.cam_lbl.config(image="",
                            text="📷\n\nCamera is off\n\nClick  ▶ Start Camera",
                            fg="#555555")
        self._st("Camera stopped.")

    def _feed(self):
        if not self._running: return
        ret, frame = self.cap.read()
        if ret:
            rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            img = Image.fromarray(rgb).resize((CAM_W, CAM_H), Image.LANCZOS)
            ph  = ImageTk.PhotoImage(img)
            self.cam_lbl.config(image=ph, text="")
            self.cam_lbl.image = ph
        self.root.after(30, self._feed)

    def _capture(self):
        if not self._running or not self.cap: return
        ret, frame = self.cap.read()
        if not ret:
            messagebox.showerror("Failed", "Could not read camera frame."); return
        self._captured = Image.fromarray(cv2.cvtColor(frame, cv2.COLOR_BGR2RGB))
        self.cap_status.config(text="✔ Captured")
        self._check_ready()
        self._st("Frame captured ✓ — fill in the details on the right and submit")

    def _upload_image(self):
        file_path = filedialog.askopenfilename(
            title="Select Image",
            filetypes=[("Image Files", "*.png *.jpg *.jpeg")]
        )
        if not file_path:
            return

        try:
            img = Image.open(file_path).convert("RGB")
            self._captured = img

            # Show preview in camera area
            preview = img.resize((CAM_W, CAM_H), Image.LANCZOS)
            ph = ImageTk.PhotoImage(preview)
            self.cam_lbl.config(image=ph, text="")
            self.cam_lbl.image = ph

            self.cap_status.config(text="✔ Image Loaded")
            self._check_ready()
            self._st("Image loaded from gallery ✓ — fill details and submit")

        except Exception as e:
            messagebox.showerror("Error", str(e))

    # ── Classification ─────────────────────────────────────────
    def _on_type(self, _=None):
        if self._clf_job: self.root.after_cancel(self._clf_job)
        self._clf_job = self.root.after(600, self._classify_live)
        self._check_ready()

    def _classify_live(self):
        text = self.vio_text.get("1.0", "end").strip()
        if len(text) < 5:
            self.cat_lbl.config(text="—"); self.fine_lbl.config(text=""); return
        def _go():
            try:
                d = requests.post(f"{API}/classify-violation",
                                  json={"text": text}, timeout=5).json()
                if d.get("success"):
                    self._last_cat  = d["category"]
                    self._last_fine = d["fine_amount"]
                    self.root.after(0, lambda: (
                        self.cat_lbl.config(text=self._last_cat, fg=NAVY),
                        self.fine_lbl.config(text=f"₹{self._last_fine}  ", fg=RED)
                    ))
            except Exception: pass
        threading.Thread(target=_go, daemon=True).start()

    def _check_ready(self):
        ok = (self._captured is not None and
              len(self.vio_text.get("1.0", "end").strip()) >= 5)
        self.sub_btn.config(state="normal" if ok else "disabled")

    # ── Submit ─────────────────────────────────────────────────
    def _submit(self):
        if not self.place_v.get().strip() or not self.city_v.get().strip():
            messagebox.showwarning("Missing", "Fill in Place and City."); return

        self.sub_btn.config(state="disabled", text="⏳  Processing...")
        self._st("Sending to AI..."); self.root.update()

        try:
            buf = io.BytesIO()
            self._captured.save(buf, format="PNG"); buf.seek(0)

            d = requests.post(f"{API}/submit-violation",
                files={"image": ("cap.png", buf, "image/png")},
                data={
                    "place":          self.place_v.get().strip(),
                    "city":           self.city_v.get().strip(),
                    "violation_text": self.vio_text.get("1.0", "end").strip(),
                    "date_time":      self.time_v.get().strip(),
                }, timeout=25).json()

            if not d.get("success"):
                err = d.get("error", "Unknown")
                msg = (f"Could not identify the driver.\n\nClassified as: "
                       f"{d.get('violation_classified_as','—')}\n\n"
                       "Ensure the face is clearly visible and well-lit."
                       if not d.get("matched", True) else err)
                messagebox.showerror("Error", msg)
                self._st(f"Failed: {err}"); return

            self._show(d)
            self._captured = None
            self.cap_status.config(text="")
            self.sub_btn.config(state="disabled")
            self._st(f"✅  {d['name']} | {d['violation_id']} | "
                     f"{d['violation_type']} | ₹{d['fine_amount']}")

        except requests.ConnectionError:
            messagebox.showerror("Offline", "Is app.py running?")
        except Exception as e:
            messagebox.showerror("Error", str(e))
        finally:
            self.sub_btn.config(text="🔍  Submit Violation")

    def _show(self, d):
        w = tk.Toplevel(self.root)
        w.title("Recorded"); w.geometry("420x500")
        w.configure(bg=WHITE); w.grab_set(); w.resizable(False, False)

        hf = tk.Frame(w, bg=NAVY, height=48)
        hf.pack(fill="x"); hf.pack_propagate(False)
        tk.Label(hf, text="✅  Violation Recorded",
                 font=("Segoe UI",12,"bold"), bg=NAVY, fg=WHITE).pack(pady=10)
        tk.Frame(w, bg=GOLD, height=3).pack(fill="x")

        fc = d.get("face_confidence", 0)
        cc = d.get("clf_confidence",  0)
        for txt, col in [
            (f"Face Match: {fc:.1f}%",          GREEN if fc >= 80 else "#E67E22"),
            (f"Violation Classified: {cc:.0f}%", NAVY),
        ]:
            tk.Label(w, text=txt, font=("Segoe UI",9,"bold"),
                     bg=col, fg=WHITE).pack(fill="x", ipady=3)

        body = tk.Frame(w, bg=WHITE)
        body.pack(fill="both", expand=True, padx=18, pady=8)
        c = d.get("citizen", {})

        tk.Label(body, text=f'"{d.get("violation_text","")}"',
                 font=("Segoe UI",9,"italic"), bg=WHITE, fg=DARK,
                 wraplength=370, justify="left").pack(anchor="w", pady=(2,8))

        for title, rows in [
            ("CITIZEN", [
                ("Name",    d.get("name","")),
                ("Aadhaar", c.get("aadhaar","")),
                ("Phone",   c.get("phone","")),
                ("License", c.get("license_no","")),
                ("Status",  c.get("status","")),
            ]),
            ("VIOLATION", [
                ("ID",       d.get("violation_id","")),
                ("Type",     d.get("violation_type","")),
                ("Location", f"{d.get('place','')} , {d.get('city','')}"),
                ("Time",     d.get("date_time","")),
                ("Fine",     f"₹{d.get('fine_amount',0)}"),
            ]),
        ]:
            tk.Label(body, text=title, font=("Segoe UI",8,"bold"),
                     bg=WHITE, fg=NAVY).pack(anchor="w", pady=(6,1))
            tk.Frame(body, bg=GOLD, height=1).pack(fill="x")
            for lb, val in rows:
                r = tk.Frame(body, bg=WHITE); r.pack(fill="x", pady=1)
                col = (RED      if val == "Blacklisted" else
                       GREEN    if val == "Clear" else
                       "#B7950B" if val == "Pending" else
                       RED      if lb  == "Fine" else DARK)
                tk.Label(r, text=f"{lb}:", font=("Segoe UI",9,"bold"),
                         bg=WHITE, fg=NAVY, width=9, anchor="w").pack(side="left")
                tk.Label(r, text=str(val), font=("Segoe UI",9),
                         bg=WHITE, fg=col).pack(side="left")

        tk.Button(w, text="Close", font=("Segoe UI",10,"bold"),
                  bg=NAVY, fg=WHITE, relief="flat",
                  padx=20, pady=6, cursor="hand2",
                  command=w.destroy).pack(pady=(6,12))

    def _ping(self):
        try:
            d = requests.get(f"{API}/health", timeout=2).json()
            self.api_lbl.config(
                text=f"⬤ API Live — {d['people_loaded']} people", fg="#2ECC71")
        except Exception:
            self.api_lbl.config(text="⬤ API Offline", fg=RED)
            messagebox.showwarning("API Offline", "Run: py -3.11 app.py")

    def _on_close(self):
        self._running = False
        if self.cap: self.cap.release()
        self.root.destroy()

    def _st(self, msg):
        self.status.config(text=f"  {msg}"); self.root.update_idletasks()

if __name__ == "__main__":
    root = tk.Tk()
    App(root)
    root.mainloop()