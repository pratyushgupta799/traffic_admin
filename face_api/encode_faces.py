import face_recognition
import os
import pickle
import numpy as np
from PIL import Image as PILImage

DATASET_PATH   = "dataset"
ENCODINGS_FILE = "encodings.pkl"

known_encodings = []
known_names     = []

print("=" * 50)
print("  TrafficAdmin — Face Encoding Script")
print("=" * 50)

person_folders = sorted([
    d for d in os.listdir(DATASET_PATH)
    if os.path.isdir(os.path.join(DATASET_PATH, d))
])

print(f"\n[INFO] Found {len(person_folders)} person folders.\n")

for person_name in person_folders:
    person_path = os.path.join(DATASET_PATH, person_name)

    image_files = [
        f for f in os.listdir(person_path)
        if f.lower().endswith((".jpg", ".jpeg", ".png"))
    ]

    if not image_files:
        print(f"[SKIP] {person_name} — folder is empty, skipping.")
        continue

    print(f"[INFO] Processing: {person_name} ({len(image_files)} images)")
    saved = 0

    for img_file in image_files:
        img_path = os.path.join(person_path, img_file)
        try:
            pil_img        = PILImage.open(img_path).convert("RGB")
            image = np.array(pil_img, dtype=np.uint8)
            face_locations = face_recognition.face_locations(image, model="hog")

            if not face_locations:
                print(f"  [WARN] No face in {img_file} — skipped")
                continue

            encodings = face_recognition.face_encodings(image, face_locations)
            for enc in encodings:
                known_encodings.append(enc)
                known_names.append(person_name)
                saved += 1

        except Exception as e:
            print(f"  [ERROR] {img_file} — {e}")

    print(f"  ✔  {saved} encoding(s) saved for {person_name}")

with open(ENCODINGS_FILE, "wb") as f:
    pickle.dump({"encodings": known_encodings, "names": known_names}, f)

print(f"\n{'=' * 50}")
print(f"  DONE — {len(known_encodings)} total encodings")
print(f"  People encoded: {len(set(known_names))}")
print(f"  Saved to: {ENCODINGS_FILE}")
print(f"{'=' * 50}")