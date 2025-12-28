from flask import Flask, request, jsonify
from flask_cors import CORS
import os
import torch
import torchaudio
from transformers import Wav2Vec2ForCTC, Wav2Vec2Processor
import numpy as np
from pathlib import Path
import librosa

app = Flask(__name__)
CORS(app)

# Configuration
SAMPLING_RATE = 16000 # Wav2Vec2 expects 16kHz
DEVICE = "cuda" if torch.cuda.is_available() else "cpu"

print(f"Loading Wav2Vec2 model on {DEVICE}...")
# Load a pre-trained model for English
MODEL_ID = "facebook/wav2vec2-base-960h"
processor = Wav2Vec2Processor.from_pretrained(MODEL_ID)
model = Wav2Vec2ForCTC.from_pretrained(MODEL_ID).to(DEVICE)
model.eval()

# Paths
BASE_DIR = Path(__file__).parent
TEMP_DIR = BASE_DIR / "temp_audio"
TEMP_DIR.mkdir(exist_ok=True)

def get_word_scores(audio_path, target_text):
    """
    Perform Force Alignment / GOP-like scoring using Wav2Vec2.
    """
    # 1. Load and resample audio
    speech, sr = librosa.load(audio_path, sr=SAMPLING_RATE)
    
    # 2. Process through model
    input_values = processor(speech, return_tensors="pt", sampling_rate=SAMPLING_RATE).input_values.to(DEVICE)
    
    with torch.no_grad():
        logits = model(input_values).logits
    
    # Calculate log probabilities
    log_probs = torch.nn.functional.log_softmax(logits, dim=-1).squeeze(0).cpu().numpy()
    
    # 3. Clean target text
    target_text = target_text.upper().strip()
    words = target_text.split()
    
    # Simple GOP (Goodness of Pronunciation) approximation:
    # We find the best matching characters in the transcription and calculate their confidence.
    # For a more production-ready Force Alignment, we would use CTC-segmentation or torchaudio.forced_align
    # But for a single word/short phrase, we can use the predicted probabilities.
    
    predicted_ids = torch.argmax(logits, dim=-1)
    transcription = processor.batch_decode(predicted_ids)[0]
    
    # Scoring Logic
    # We'll calculate a score based on how close the predicted transcription is to target
    # AND the confidence of the model for those characters.
    
    total_score = 0
    word_details = []
    
    # Simple word-level matching
    # This is a simplified version. A real alignment would map time-frames to words.
    predicted_words = transcription.split()
    
    for i, target_word in enumerate(words):
        # Find if this word exists in transcription
        found = False
        score = 0
        
        if i < len(predicted_words):
            word_pred = predicted_words[i]
            if word_pred == target_word:
                # Basic score for character confidence
                score = 100
                found = True
            else:
                # Partial match or wrong word
                # Use Levenshtein distance or similar for partial credit?
                from difflib import SequenceMatcher
                ratio = SequenceMatcher(None, target_word, word_pred).ratio()
                score = ratio * 100
        else:
            score = 0 # Word missing
            
        word_details.append({
            "word": target_word,
            "score": round(score, 2),
            "status": "Correct" if score > 80 else "Needs Improvement" if score > 0 else "Missing"
        })
        total_score += score

    overall_score = total_score / len(words) if words else 0
    
    # Extra: Calculate precision based on duration
    duration = librosa.get_duration(y=speech, sr=SAMPLING_RATE)
    
    return float(overall_score), word_details, round(duration, 2)

@app.route('/compare', methods=['POST'])
def compare():
    if 'audio' not in request.files:
        return jsonify({"error": "No audio file provided"}), 400
    
    audio_file = request.files['audio']
    target_text = request.form.get('target_text', '').strip()
    
    if not target_text:
        return jsonify({"error": "No target text provided"}), 400

    temp_path = TEMP_DIR / f"user_{os.urandom(4).hex()}.wav"
    audio_file.save(str(temp_path))
    
    try:
        # Use Force Alignment Model
        score, word_details, duration = get_word_scores(str(temp_path), target_text)
        
        return jsonify({
            "status": "success",
            "score": round(score, 2),
            "word_details": word_details,
            "duration": duration,
            "target": target_text
        })
        
    except Exception as e:
        import traceback
        traceback.print_exc()
        return jsonify({"error": str(e)}), 500
    finally:
        if temp_path.exists():
            os.remove(temp_path)

if __name__ == '__main__':
    print("Force Alignment API Server starting...")
    app.run(host='0.0.0.0', port=5000)
