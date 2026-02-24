"""
test_recognize.py
─────────────────
Quick local test — runs recognition directly without Flask.
Use this to verify your encodings.pkl is working before
starting the API.

Usage:
    python test_recognize.py

It will ask you to enter an image path, then print the result.
"""

import face_recognition
import pickle
import numpy as np
import os

ENCODINGS_FILE   = "encodings.pkl"
CONFIDENCE_THRESHOLD = 0.52

# ── Load encodings ─────────────────────────────────────────────
if not os.path.exists(ENCODINGS_FILE):
    print("[ERROR] encodings.pkl not found. Run encode_faces.py first.")
    exit()

with open(ENCODINGS_FILE, "rb") as f:
    data = pickle.load(f)

known_encodings = data["encodings"]
known_names     = data["names"]
print(f"[OK] Loaded {len(known_encodings)} encodings for {len(set(known_names))} people.")
print(f"     People: {', '.join(sorted(set(known_names)))}\n")

# ── Test loop ──────────────────────────────────────────────────
while True:
    img_path = input("Enter image path to test (or 'q' to quit): ").strip().strip('"')

    if img_path.lower() == "q":
        print("Exiting.")
        break

    if not os.path.exists(img_path):
        print(f"  [ERROR] File not found: {img_path}\n")
        continue

    print(f"  Processing {os.path.basename(img_path)} ...")

    image          = face_recognition.load_image_file(img_path)
    face_locations = face_recognition.face_locations(image, model="hog")

    if not face_locations:
        print("  [RESULT] ❌ No face detected in this image.\n")
        continue

    encodings = face_recognition.face_encodings(image, face_locations)
    if not encodings:
        print("  [RESULT] ❌ Face detected but encoding failed.\n")
        continue

    query_enc  = encodings[0]
    distances  = face_recognition.face_distance(known_encodings, query_enc)
    best_idx   = int(np.argmin(distances))
    best_dist  = float(distances[best_idx])
    confidence = round((1.0 - best_dist) * 100, 2)

    print(f"\n  ┌──────────────────────────────────────┐")
    if best_dist <= CONFIDENCE_THRESHOLD:
        print(f"  │  ✅ MATCH FOUND                       │")
        print(f"  │  Name:       {known_names[best_idx]:<24} │")
        print(f"  │  Confidence: {confidence}%{'':<22}│")
        print(f"  │  Distance:   {best_dist:.4f}{'':<22}│")
    else:
        print(f"  │  ❌ NO MATCH — Unknown person         │")
        print(f"  │  Closest:    {known_names[best_idx]:<24} │")
        print(f"  │  Confidence: {confidence}% (too low){'':<12}│")
    print(f"  └──────────────────────────────────────┘\n")