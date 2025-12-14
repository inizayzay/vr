"""
Generate Reference MFCC for Voice Recognition Questions
This script processes reference audio files and creates JSON MFCC files for VB.NET
"""

import os
import numpy as np
import librosa
from pathlib import Path
import json


class ReferenceGenerator:
    """Generate MFCC references from audio files"""
    
    def __init__(self, n_mfcc=13, sr=22050, n_fft=2048, hop_length=512):
        self.n_mfcc = n_mfcc
        self.sr = sr
        self.n_fft = n_fft
        self.hop_length = hop_length
    
    def extract_mfcc_from_wav(self, wav_path):
        """Extract MFCC from WAV file"""
        # Load audio
        audio, sr = librosa.load(wav_path, sr=self.sr)
        
        # Extract MFCC
        mfcc = librosa.feature.mfcc(
            y=audio,
            sr=sr,
            n_mfcc=self.n_mfcc,
            n_fft=self.n_fft,
            hop_length=self.hop_length
        )
        
        return mfcc
    
    def save_mfcc_as_json(self, mfcc, output_path, question_id, text):
        """Save MFCC as JSON for VB.NET"""
        # Convert to list for JSON serialization
        mfcc_list = mfcc.tolist()
        
        # Create JSON structure
        data = {
            "question_id": question_id,
            "text": text,
            "mfcc": mfcc_list,
            "sample_rate": self.sr,
            "n_mfcc": self.n_mfcc,
            "shape": list(mfcc.shape)
        }
        
        # Save to JSON
        with open(output_path, 'w') as f:
            json.dump(data, f, indent=2)
        
        print(f"✓ Saved: {output_path}")
    
    def process_reference_audio(self, wav_path, output_dir, question_id, text):
        """Process single reference audio file"""
        # Extract MFCC
        mfcc = self.extract_mfcc_from_wav(wav_path)
        
        # Create output filename
        output_filename = f"{text.lower()}_mfcc.json"
        output_path = Path(output_dir) / output_filename
        
        # Save as JSON
        self.save_mfcc_as_json(mfcc, output_path, question_id, text)
        
        return output_path
    
    def generate_all_references(self, reference_dir, output_dir):
        """Generate MFCC for all reference audio files"""
        reference_dir = Path(reference_dir)
        output_dir = Path(output_dir)
        
        # Create output directory
        output_dir.mkdir(parents=True, exist_ok=True)
        
        # Find all WAV files
        wav_files = list(reference_dir.glob("*_ref.wav"))
        
        if not wav_files:
            print(f"❌ No reference WAV files found in {reference_dir}")
            print("   Expected format: <word>_ref.wav (e.g., cup_ref.wav)")
            return
        
        print(f"Found {len(wav_files)} reference audio files")
        print("=" * 60)
        
        results = []
        for wav_file in wav_files:
            # Extract text from filename (remove _ref.wav)
            text = wav_file.stem.replace("_ref", "")
            
            print(f"\nProcessing: {wav_file.name}")
            print(f"  Text: {text}")
            
            try:
                # Process
                output_path = self.process_reference_audio(
                    str(wav_file),
                    str(output_dir),
                    question_id=0,  # Will be updated from DB
                    text=text
                )
                
                results.append({
                    'text': text,
                    'wav_file': str(wav_file),
                    'json_file': str(output_path),
                    'status': 'success'
                })
                
            except Exception as e:
                print(f"✗ Error: {str(e)}")
                results.append({
                    'text': text,
                    'wav_file': str(wav_file),
                    'status': 'error',
                    'error': str(e)
                })
        
        print("\n" + "=" * 60)
        print("SUMMARY")
        print("=" * 60)
        successful = sum(1 for r in results if r['status'] == 'success')
        print(f"Total: {len(results)}")
        print(f"Successful: {successful}")
        print(f"Failed: {len(results) - successful}")
        
        return results


def main():
    """Main function"""
    print("=" * 60)
    print("REFERENCE MFCC GENERATOR")
    print("=" * 60)
    
    # Get script directory
    script_dir = Path(__file__).parent
    
    # Setup paths
    reference_dir = script_dir / "dataset" / "references"
    output_dir = script_dir / "references"
    
    print(f"Reference Audio Dir: {reference_dir}")
    print(f"Output JSON Dir    : {output_dir}")
    print("=" * 60)
    
    # Check if reference directory exists
    if not reference_dir.exists():
        print(f"\n❌ ERROR: Reference directory not found!")
        print(f"   Please create: {reference_dir}")
        print(f"   And add reference audio files:")
        print(f"     - cup_ref.wav")
        print(f"     - glass_ref.wav")
        print(f"     - bottle_ref.wav")
        print(f"     - etc.")
        return
    
    # Initialize generator
    generator = ReferenceGenerator(
        n_mfcc=13,
        sr=22050,
        n_fft=2048,
        hop_length=512
    )
    
    # Generate all references
    results = generator.generate_all_references(reference_dir, output_dir)
    
    if results:
        print("\n✅ Reference generation complete!")
        print(f"\nJSON files saved to: {output_dir}")
        print("\nNext steps:")
        print("1. Copy JSON files to your VB.NET project folder")
        print("2. Install NuGet packages: NAudio, Accord.Audio, Newtonsoft.Json")
        print("3. Add the VB.NET modules to your project")
        print("4. Modify Form2 and Form3 as per implementation plan")


if __name__ == "__main__":
    main()
