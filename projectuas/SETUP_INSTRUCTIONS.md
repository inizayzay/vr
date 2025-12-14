# Setup Instructions for MFCC-DTW Voice Recognition

## 📋 Prerequisites

- Visual Studio 2019 or later
- Python 3.8+ (for generating reference MFCC)
- MySQL database running (XAMPP)

---

## 🔧 Step 1: Install NuGet Packages

Open **Package Manager Console** in Visual Studio and run:

```powershell
Install-Package NAudio
Install-Package Newtonsoft.Json
```

**Note:** Accord.Audio is NOT needed anymore!

---

## 🎤 Step 2: Record Reference Audio

1. Create folder: `dataset/references/`
2. Record clear pronunciation for each word:
   - `cup_ref.wav`
   - `glass_ref.wav`
   - `bottle_ref.wav`
   - etc.

**Recording specs:**

- Format: WAV
- Sample rate: 22050 Hz
- Channels: Mono
- Duration: 2-3 seconds

---

## 🐍 Step 3: Generate Reference MFCC (Python)

```bash
# Install Python dependencies
pip install librosa numpy

# Generate reference MFCC files
python generate_references.py
```

This will create JSON files in `references/` folder:

- `cup_mfcc.json`
- `glass_mfcc.json`
- etc.

---

## 📁 Step 4: Add Files to VB.NET Project

1. **Add VB modules** to your project:

   - `AudioRecorder.vb`
   - `MFCCExtractor.vb`
   - `DTWComparator.vb`

2. **Copy reference JSON files** to project folder:

   - Create `references/` folder in project root
   - Copy all `*_mfcc.json` files there

3. **Set JSON files properties**:
   - Build Action: `Content`
   - Copy to Output Directory: `Copy if newer`

---

## 🗄️ Step 5: Update Database Schema

Run this SQL in your MySQL database:

```sql
ALTER TABLE results
ADD COLUMN dtw_score DECIMAL(5,2) DEFAULT NULL,
ADD COLUMN audio_file_path VARCHAR(255) DEFAULT NULL;
```

---

## ✅ Step 6: Test the Application

1. **Build the project** (Ctrl+Shift+B)
2. **Run the application** (F5)
3. **Test flow:**
   - Enter name → Form1
   - Tap to speak → Form2 (records audio)
   - View results → Form3 (shows DTW + Word scores)

---

## 🐛 Troubleshooting

### Error: "NAudio not found"

```powershell
Install-Package NAudio
```

### Error: "Accord.Audio not found"

```powershell
Install-Package Accord.Audio
```

### Error: "Reference MFCC not found"

- Check if `references/` folder exists in output directory
- Verify JSON files are copied (check file properties)
- Ensure filename matches: `<word>_mfcc.json`

### Error: "Audio recording failed"

- Check microphone permissions
- Verify default microphone is set in Windows

### Low DTW scores even with correct pronunciation

- Re-record reference audio with clearer pronunciation
- Adjust DTW similarity function alpha parameter in `DTWComparator.vb`

---

## 📊 Expected Results

**Good pronunciation:**

- DTW Score: 70-100%
- Word Accuracy: 100%
- Combined Score: 79-100%

**Poor pronunciation:**

- DTW Score: 30-60%
- Word Accuracy: 100%
- Combined Score: 51-72%

**Wrong word:**

- DTW Score: 0-30%
- Word Accuracy: 0%
- Combined Score: 0-21%

---

## 🎯 Next Steps

1. Test with multiple users
2. Collect more reference audio samples
3. Fine-tune DTW parameters
4. Add more questions to database

---

**Happy Coding! 🚀**
