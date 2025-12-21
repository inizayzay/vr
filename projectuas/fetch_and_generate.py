"""
Batch Dataset Generation for MFCC-DTW Voice Recognition
This script filters a CSV of audio URLs, downloads the MP3s, 
and generates MFCC JSON files for VB.NET.
"""

import os
import csv
import json
import librosa
import numpy as np
import requests
from pathlib import Path
import time
from generate_references import ReferenceGenerator

class BatchGenerator:
    def __init__(self, csv_path, target_words, output_audio_dir, output_json_dir):
        self.csv_path = Path(csv_path)
        self.target_words = [w.lower().strip() for w in target_words]
        self.output_audio_dir = Path(output_audio_dir)
        self.output_json_dir = Path(output_json_dir)
        self.generator = ReferenceGenerator(n_mfcc=13, sr=22050)
        
        # Ensure directories exist
        self.output_audio_dir.mkdir(parents=True, exist_ok=True)
        self.output_json_dir.mkdir(parents=True, exist_ok=True)

    def fetch_word_urls(self):
        """Parse CSV and find URLs for target words"""
        word_map = {}
        target_set = set(self.target_words)
        
        print(f"Reading CSV: {self.csv_path}")
        try:
            with open(self.csv_path, mode='r', encoding='utf-8') as f:
                reader = csv.DictReader(f)
                for row in reader:
                    word = row['word'].lower().strip()
                    if word in target_set:
                        # Prefer US audio, fallback to GB
                        url = row.get('us_audio_url') or row.get('gb_audio_url')
                        if url:
                            word_map[word] = url
        except Exception as e:
            print(f"Error reading CSV: {e}")
            return {}
            
        print(f"Found {len(word_map)} out of {len(target_set)} target words.")
        missing = target_set - set(word_map.keys())
        if missing:
            print(f"Missing words: {', '.join(sorted(missing))}")
            
        return word_map

    def download_audio(self, word, url):
        """Download MP3 file"""
        # Ensure we use underscorized name for the filename
        safe_word = word.replace(" ", "_")
        output_path = self.output_audio_dir / f"{safe_word}_ref.mp3"
        
        if output_path.exists():
            print(f"  - already exists: {output_path.name}")
            return output_path
            
        try:
            print(f"  - downloading {word} from {url}...")
            headers = {
                'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36'
            }
            response = requests.get(url, timeout=15, headers=headers)
            response.raise_for_status()
            with open(output_path, 'wb') as f:
                f.write(response.content)
            print(f"  - saved to {output_path.name}")
            return output_path
        except Exception as e:
            print(f"  - error downloading {word}: {e}")
            return None

    def process_all(self):
        """Main execution flow"""
        word_map = self.fetch_word_urls()
        
        results = []
        for word in self.target_words:
            if word not in word_map:
                continue
                
            url = word_map[word]
            print(f"\nProcessing: {word}")
            
            # 1. Download
            audio_path = self.download_audio(word, url)
            if not audio_path:
                continue
                
            # 2. Extract MFCC
            try:
                # librosa can load MP3 directly
                # We normalize word for filename consistency (cake -> cake_mfcc.json)
                safe_word = word.replace(" ", "_")
                
                # We need to adapt the ReferenceGenerator to handle MP3 if it doesn't
                # Actually librosa.load handled by librosa.feature.mfcc -> internally uses audioread
                audio, sr = librosa.load(str(audio_path), sr=self.generator.sr)
                
                mfcc = librosa.feature.mfcc(
                    y=audio,
                    sr=sr,
                    n_mfcc=self.generator.n_mfcc,
                    n_fft=self.generator.n_fft,
                    hop_length=self.generator.hop_length
                )
                
                json_path = self.output_json_dir / f"{safe_word}_mfcc.json"
                self.generator.save_mfcc_as_json(mfcc, str(json_path), 0, word)
                
                results.append(word)
                # Small delay to be nice to servers
                time.sleep(0.5)
                
            except Exception as e:
                print(f"  - error processing {word}: {e}")
                
        print(f"\nFinished! Successfully processed {len(results)} words.")
        return results

if __name__ == "__main__":
    WORDS = [
        "Cup", "Plate", "Spoon", "Fork", "Bag", "Shoes", "Shirt", "Pants", "Hat", "Gloves", 
        "Chicken", "Goat", "Horse", "Duck", "Frog", "Bed", "Door", "Window", "Lamp", "Clock", 
        "Ice cream", "Candy", "Chocolate", "Cookie", "Honey", "Brother", "Sister", "Mother", 
        "Father", "Baby", "Run", "Jump", "Sleep", "Eat", "Drink", "Smile", "Cry", "Sing", 
        "Dance", "Play", "Beach", "Mountain", "River", "Forest", "Park", "Cupcake", "Pencil", 
        "Crayon", "Notebook", "Backpack"
    ]
    
    # Paths relative to script
    SCRIPT_DIR = Path(__file__).parent
    CSV_PATH = SCRIPT_DIR / "archive" / "text_audio_urls.csv"
    AUDIO_DIR = SCRIPT_DIR / "dataset" / "references"
    JSON_DIR = SCRIPT_DIR / "references"
    
    batch = BatchGenerator(CSV_PATH, WORDS, AUDIO_DIR, JSON_DIR)
    batch.process_all()
