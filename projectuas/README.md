# 🎯 MFCC-DTW Voice Recognition System

Sistem pengenalan suara berbasis MFCC (Mel-Frequency Cepstral Coefficients) dan DTW (Dynamic Time Warping) untuk penilaian pengucapan yang akurat.

---

## 📁 File Structure

```
projectuas/
├── AudioRecorder.vb          ✅ Audio recording (NAudio)
├── MFCCExtractor.vb          ✅ MFCC extraction (Accord.NET)
├── DTWComparator.vb          ✅ DTW algorithm
├── Form1.vb                  ✅ Login/Registration
├── Form2.vb                  ✅ Voice recording + recognition
├── Form3.vb                  ✅ DTW scoring + results
├── DatabaseModul.vb          ✅ Database connection
├── generate_references.py    ✅ Python reference generator
├── database_migration.sql    ✅ Database schema update
├── SETUP_INSTRUCTIONS.md     ✅ Setup guide
└── README.md                 📄 This file

dataset/
└── references/               📁 Reference audio (WAV files)
    ├── cup_ref.wav
    ├── glass_ref.wav
    └── ...

references/                   📁 Reference MFCC (JSON files)
├── cup_mfcc.json
├── glass_mfcc.json
└── ...

temp/                         📁 User recordings (auto-created)
└── user_*.wav
```

---

## 🚀 Quick Start

### 1. Install Dependencies

**NuGet Packages:**

```powershell
Install-Package NAudio
Install-Package Accord.Audio
Install-Package Newtonsoft.Json
```

**Python (for reference generation):**

```bash
pip install librosa numpy
```

### 2. Database Setup

```sql
-- Run database_migration.sql
mysql -u root -p db_vr < database_migration.sql
```

### 3. Generate Reference MFCC

```bash
# Record reference audio first (cup_ref.wav, glass_ref.wav, etc.)
# Then generate MFCC:
python generate_references.py
```

### 4. Run Application

1. Build project (Ctrl+Shift+B)
2. Run (F5)
3. Test with voice input

---

## 🎯 How It Works

### Flow Diagram

```
┌─────────────┐
│   Form1     │  Login/Register
│  (Input)    │
└──────┬──────┘
       │
       v
┌─────────────┐
│   Form2     │  1. User taps "Speak"
│  (Record)   │  2. Start audio recording (NAudio)
│             │  3. Start speech recognition
│             │  4. Stop recording when done
└──────┬──────┘  5. Save WAV file
       │
       v
┌─────────────┐
│   Form3     │  1. Extract MFCC from user audio (Accord.NET)
│  (Scoring)  │  2. Load reference MFCC (JSON)
│             │  3. Calculate DTW distance
│             │  4. Convert to similarity score (0-100)
│             │  5. Combine: 70% DTW + 30% Word accuracy
│             │  6. Display results
└─────────────┘
```

### Scoring Formula

```
Combined Score = (DTW Similarity × 0.7) + (Word Accuracy × 0.3)
```

**Example:**

- DTW Similarity: 85%
- Word Accuracy: 100%
- **Combined Score: 89.5%**

---

## 📊 Components

### 1. AudioRecorder.vb

- Records from microphone using NAudio
- Saves as WAV (22050 Hz, mono, 16-bit)
- Auto-generates temp file names

### 2. MFCCExtractor.vb

- Extracts MFCC using Accord.NET
- Loads reference MFCC from JSON
- Handles resampling and mono conversion

### 3. DTWComparator.vb

- Implements DTW algorithm
- Calculates Euclidean distance between frames
- Converts distance to similarity score (0-100)

### 4. Form2.vb (Modified)

- Starts recording on "Tap to Speak"
- Stops recording when recognition completes
- Passes WAV path to Form3

### 5. Form3.vb (Modified)

- Calculates DTW similarity
- Combines with word accuracy
- Saves both scores to database
- Displays detailed feedback

---

## 🗄️ Database Schema

### results Table

| Column              | Type             | Description                 |
| ------------------- | ---------------- | --------------------------- |
| id                  | INT              | Primary key                 |
| user_id             | INT              | User ID                     |
| question_id         | INT              | Question ID                 |
| word_accuracy       | DECIMAL(5,2)     | Word matching score (0-100) |
| phoneme_accuracy    | DECIMAL(5,2)     | Placeholder                 |
| final_score         | DECIMAL(5,2)     | Combined score              |
| **dtw_score**       | **DECIMAL(5,2)** | **DTW similarity (0-100)**  |
| **audio_file_path** | **VARCHAR(255)** | **Path to WAV file**        |
| created_at          | TIMESTAMP        | Timestamp                   |

---

## 🎤 Recording Reference Audio

### Requirements:

- Clear pronunciation
- No background noise
- 2-3 seconds duration
- WAV format, 22050 Hz, mono

### Naming Convention:

```
<word>_ref.wav
```

**Examples:**

- cup_ref.wav
- glass_ref.wav
- bottle_ref.wav

### Tools:

- Audacity (free)
- Windows Voice Recorder
- Any audio recording software

---

## 🧪 Testing

### Expected Results:

**Perfect pronunciation:**

- DTW: 80-100%
- Word: 100%
- Combined: 86-100%

**Good pronunciation:**

- DTW: 60-80%
- Word: 100%
- Combined: 72-86%

**Poor pronunciation:**

- DTW: 30-60%
- Word: 100%
- Combined: 51-72%

**Wrong word:**

- DTW: 0-30%
- Word: 0%
- Combined: 0-21%

---

## 🐛 Troubleshooting

### "Reference MFCC not found"

- Check `references/` folder exists in output directory
- Verify JSON files are copied (Build Action: Content)
- Ensure filename matches: `<word>_mfcc.json`

### "Audio recording failed"

- Check microphone permissions
- Verify default microphone in Windows settings
- Install NAudio package

### Low DTW scores

- Re-record reference audio with clearer pronunciation
- Adjust alpha parameter in `DTWComparator.vb` (line ~95)
- Check audio quality (no noise, clear voice)

### "Accord.Audio not found"

```powershell
Install-Package Accord.Audio
```

---

## 📚 References

- [MFCC Explanation](https://en.wikipedia.org/wiki/Mel-frequency_cepstrum)
- [DTW Algorithm](https://en.wikipedia.org/wiki/Dynamic_time_warping)
- [NAudio Documentation](https://github.com/naudio/NAudio)
- [Accord.NET Framework](http://accord-framework.net/)

---

## 👨‍💻 Development

### Adding New Questions

1. Add to database:

```sql
INSERT INTO questions (text, level) VALUES ('bottle', 'Easy');
```

2. Record reference audio:

```
dataset/references/bottle_ref.wav
```

3. Generate MFCC:

```bash
python generate_references.py
```

4. Copy JSON to `references/` folder

---

## 📝 License

Educational project for Visual Programming course.

---

**Created with ❤️ for accurate voice pronunciation assessment**
