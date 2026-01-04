from flask import Flask, request, jsonify, send_file
from flask_cors import CORS
import os
import torch
import torchaudio
from transformers import Wav2Vec2ForCTC, Wav2Vec2Processor
import numpy as np
from pathlib import Path
import librosa
from g2p_en import G2p
import pyttsx3
import tempfile
import threading
import nltk

# Ensure required NLTK data is downloaded
nltk.download('averaged_perceptron_tagger_eng')
nltk.download('cmudict')

app = Flask(__name__)
CORS(app)

# Configuration
SAMPLING_RATE = 16000 # Wav2Vec2 expects 16kHz
DEVICE = "cuda" if torch.cuda.is_available() else "cpu"

print(f"Loading Wav2Vec2 model on {DEVICE}...")
MODEL_ID = "facebook/wav2vec2-base-960h"
processor = Wav2Vec2Processor.from_pretrained(MODEL_ID)
model = Wav2Vec2ForCTC.from_pretrained(MODEL_ID).to(DEVICE)
model.eval()

# Initialize G2P for phoneme conversion
g2p = G2p()

# Paths
BASE_DIR = Path(__file__).parent
TEMP_DIR = BASE_DIR / "temp_audio"
TEMP_DIR.mkdir(exist_ok=True)

def get_phonemes(text):
    """Convert text to phonetic representation."""
    return [p for p in g2p(text) if p.strip()]

def get_detailed_scores(audio_path, target_text):
    """
    Perform granular scoring (Word and Phoneme level)
    """
    # 1. Load and resample audio
    speech, sr = librosa.load(audio_path, sr=SAMPLING_RATE)
    
    # 2. Process through model
    inputs = processor(speech, return_tensors="pt", sampling_rate=SAMPLING_RATE)
    input_values = inputs.input_values.to(DEVICE)
    
    with torch.no_grad():
        logits = model(input_values).logits
    
    # Calculate probabilities
    probs = torch.nn.functional.softmax(logits, dim=-1).squeeze(0).cpu().numpy()
    predicted_ids = torch.argmax(logits, dim=-1).squeeze(0).cpu().numpy()
    
    # Decoding
    transcription = processor.decode(predicted_ids).upper()
    
    # Target Clean-up
    target_text = target_text.upper().strip()
    target_words = target_text.split()
    target_phonemes_list = [get_phonemes(w) for w in target_words]
    
    # Predicted Words
    pred_words = transcription.split()
    
    word_details = []
    total_score = 0
    
    # Simplified Word & Phoneme Alignment
    # In a full system, we'd use CTC-segmentation for time-alignment
    for i, target_word in enumerate(target_words):
        word_score = 0
        phonemes_feedback = []
        expected_phonemes = target_phonemes_list[i]
        
        if i < len(pred_words):
            pred_word = pred_words[i]
            if pred_word == target_word:
                word_score = 100
                # If word is correct, check phoneme "purity" (simplified)
                for p in expected_phonemes:
                    phonemes_feedback.append({"phoneme": p, "score": 100, "status": "Good"})
            else:
                # Partial match logic
                from difflib import SequenceMatcher
                ratio = SequenceMatcher(None, target_word, pred_word).ratio()
                word_score = ratio * 100
                
                # Phoneme level feedback simulation based on word overlap
                for p in expected_phonemes:
                    # Very simplified: if word part exists, phoneme is okay
                    p_score = word_score if len(p) > 1 else 100 # vowels usually okay
                    phonemes_feedback.append({
                        "phoneme": p, 
                        "score": round(p_score, 2),
                        "status": "Good" if p_score > 80 else "Needs Work"
                    })
        else:
            word_score = 0
            for p in expected_phonemes:
                phonemes_feedback.append({"phoneme": p, "score": 0, "status": "Missing"})
        
        word_details.append({
            "word": target_word,
            "score": round(word_score, 2),
            "status": "Correct" if word_score > 80 else "Needs Improvement" if word_score > 0 else "Missing",
            "phonemes": phonemes_feedback
        })
        total_score += word_score

    overall_score = total_score / len(target_words) if target_words else 0
    duration = librosa.get_duration(y=speech, sr=SAMPLING_RATE)
    
    return float(overall_score), word_details, round(duration, 2)

@app.route('/compare', methods=['POST'])
def compare():
    if 'audio' not in request.files:
        return jsonify({"error": "No audio file provided"}), 400
    
    audio_file = request.files['audio']
    target_text = request.form.get('target_text', '').strip()
    
    temp_path = TEMP_DIR / f"user_{os.urandom(4).hex()}.wav"
    audio_file.save(str(temp_path))
    
    try:
        score, word_details, duration = get_detailed_scores(str(temp_path), target_text)
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

@app.route('/tts', methods=['GET'])
def tts():
    text = request.args.get('text', '').strip()
    if not text:
        return jsonify({"error": "No text provided"}), 400
    
    # Use pyttsx3 to generate WAV
    temp_wav = TEMP_DIR / f"tts_{os.urandom(4).hex()}.wav"
    
    def run_tts():
        engine = pyttsx3.init()
        # Set property before saving
        engine.setProperty('rate', 150)
        engine.save_to_file(text, str(temp_wav))
        engine.runAndWait()
        # Explicitly stop the engine to release resources
        engine.stop()

    # pyttsx3 runAndWait can sometimes block or have threading issues in Flask
    # but for local development, it's usually fine.
    try:
        t = threading.Thread(target=run_tts)
        t.start()
        t.join(5) # Wait max 5 seconds
        
        if temp_wav.exists():
            return send_file(str(temp_wav), mimetype="audio/wav")
        else:
            return jsonify({"error": "Failed to generate TTS"}), 500
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    print("Advanced Voice API Server starting...")
    app.run(host='0.0.0.0', port=5000, threaded=True)
