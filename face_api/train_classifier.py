"""
train_classifier.py
───────────────────
Trains a text classification model on traffic_data.csv
Saves: violation_classifier.pkl (used by app.py at runtime)

Run once:
    py -3.11 train_classifier.py
"""

import pandas as pd
import pickle
from sklearn.pipeline import Pipeline
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.linear_model import LogisticRegression
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report

# ── Category mapping: CSV label → our fine table key ──────────
CATEGORY_MAP = {
    "Speeding":         "Overspeeding",
    "SignalJumping":    "Signal Jump",
    "DrunkDriving":     "Drunk Driving",
    "NoHelmet":         "No Helmet",
    "ParkingViolation": "Parking Violation",
    "NoLicense":        "No License",
}

FINE_TABLE = {
    "Overspeeding":      1000,
    "Signal Jump":       500,
    "Drunk Driving":     5000,
    "No Helmet":         500,
    "Parking Violation": 300,
    "No License":        1000,
    "Wrong Lane":        300,
    "No Seatbelt":       500,
    "Triple Riding":     1000,
    "General":           500,
}

print("=" * 50)
print("  TrafficAdmin — Violation Classifier Training")
print("=" * 50)

# ── Load data ──────────────────────────────────────────────────
df = pd.read_csv("traffic_data.csv").dropna()
df.columns = df.columns.str.strip()
df["Category"] = df["Category"].str.strip().map(CATEGORY_MAP)
df = df.dropna(subset=["Category"])

print(f"\n[INFO] Loaded {len(df)} samples")
print(f"[INFO] Categories:\n{df['Category'].value_counts().to_string()}\n")

X = df["Text"].tolist()
y = df["Category"].tolist()

X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.2, random_state=42, stratify=y)

# ── Build pipeline ─────────────────────────────────────────────
pipeline = Pipeline([
    ("tfidf", TfidfVectorizer(
        ngram_range=(1, 2),
        max_features=5000,
        sublinear_tf=True,
        stop_words="english"
    )),
    ("clf", LogisticRegression(
        max_iter=500,
        C=5.0,
        solver="lbfgs",
    )),
])

pipeline.fit(X_train, y_train)

# ── Evaluate ───────────────────────────────────────────────────
y_pred = pipeline.predict(X_test)
print("[RESULTS] Classification Report:")
print(classification_report(y_test, y_pred))

# ── Save ───────────────────────────────────────────────────────
model_data = {
    "pipeline":     pipeline,
    "category_map": CATEGORY_MAP,
    "fine_table":   FINE_TABLE,
    "classes":      list(pipeline.classes_),
}
with open("violation_classifier.pkl", "wb") as f:
    pickle.dump(model_data, f)

print("[DONE] Model saved to violation_classifier.pkl")
print("\nTest some phrases:")
tests = [
    "Driver going 120 in a 60 zone",
    "Ran red light at junction",
    "Bike rider without helmet",
    "Drunk driver swerving",
    "Car parked on footpath",
    "No license found on driver",
]
for t in tests:
    pred  = pipeline.predict([t])[0]
    proba = pipeline.predict_proba([t]).max() * 100
    fine  = FINE_TABLE.get(pred, 500)
    print(f"  '{t}'\n   → {pred}  ({proba:.1f}% confidence)  Fine: ₹{fine}\n")