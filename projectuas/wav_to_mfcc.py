"""
Script untuk mengkonversi file WAV ke MFCC (Mel-Frequency Cepstral Coefficients)
untuk keperluan dataset audio processing dan machine learning.
"""

import os
import numpy as np
import librosa
import librosa.display
import matplotlib.pyplot as plt
from pathlib import Path
import json
import csv


class WavToMFCC:
    """
    Class untuk mengkonversi file WAV ke MFCC features
    """
    
    def __init__(self, n_mfcc=13, n_fft=2048, hop_length=512, sr=22050):
        """
        Inisialisasi parameter MFCC
        
        Args:
            n_mfcc (int): Jumlah koefisien MFCC yang akan diekstrak (default: 13)
            n_fft (int): Panjang FFT window (default: 2048)
            hop_length (int): Jumlah sample antara frame (default: 512)
            sr (int): Sample rate untuk load audio (default: 22050 Hz)
        """
        self.n_mfcc = n_mfcc
        self.n_fft = n_fft
        self.hop_length = hop_length
        self.sr = sr
    
    def extract_mfcc(self, audio_path):
        """
        Ekstrak MFCC dari file audio WAV
        
        Args:
            audio_path (str): Path ke file WAV
            
        Returns:
            tuple: (mfcc_features, audio_data, sample_rate)
                - mfcc_features: numpy array dengan shape (n_mfcc, time_steps)
                - audio_data: numpy array dari audio signal
                - sample_rate: sample rate dari audio
        """
        # Load audio file
        audio, sr = librosa.load(audio_path, sr=self.sr)
        
        # Ekstrak MFCC
        mfcc = librosa.feature.mfcc(
            y=audio,
            sr=sr,
            n_mfcc=self.n_mfcc,
            n_fft=self.n_fft,
            hop_length=self.hop_length
        )
        
        return mfcc, audio, sr
    
    def extract_mfcc_with_delta(self, audio_path):
        """
        Ekstrak MFCC beserta delta dan delta-delta (turunan pertama dan kedua)
        
        Args:
            audio_path (str): Path ke file WAV
            
        Returns:
            dict: Dictionary berisi mfcc, delta, dan delta_delta
        """
        mfcc, audio, sr = self.extract_mfcc(audio_path)
        
        # Hitung delta (turunan pertama)
        delta_mfcc = librosa.feature.delta(mfcc)
        
        # Hitung delta-delta (turunan kedua)
        delta2_mfcc = librosa.feature.delta(mfcc, order=2)
        
        return {
            'mfcc': mfcc,
            'delta': delta_mfcc,
            'delta_delta': delta2_mfcc,
            'audio': audio,
            'sr': sr
        }
    
    def get_mfcc_statistics(self, mfcc):
        """
        Hitung statistik dari MFCC (mean, std, min, max)
        
        Args:
            mfcc (numpy.ndarray): MFCC features
            
        Returns:
            dict: Dictionary berisi statistik
        """
        return {
            'mean': np.mean(mfcc, axis=1),
            'std': np.std(mfcc, axis=1),
            'min': np.min(mfcc, axis=1),
            'max': np.max(mfcc, axis=1)
        }
    
    def visualize_mfcc(self, mfcc, sr, save_path=None):
        """
        Visualisasi MFCC
        
        Args:
            mfcc (numpy.ndarray): MFCC features
            sr (int): Sample rate
            save_path (str, optional): Path untuk menyimpan visualisasi
        """
        plt.figure(figsize=(12, 6))
        librosa.display.specshow(
            mfcc,
            x_axis='time',
            sr=sr,
            hop_length=self.hop_length,
            cmap='viridis'
        )
        plt.colorbar(format='%+2.0f dB')
        plt.title('MFCC')
        plt.xlabel('Time')
        plt.ylabel('MFCC Coefficients')
        plt.tight_layout()
        
        if save_path:
            plt.savefig(save_path, dpi=300, bbox_inches='tight')
            print(f"Visualisasi disimpan di: {save_path}")
        else:
            plt.show()
        
        plt.close()
    
    def export_to_csv(self, mfcc, delta, delta2, stats, output_path):
        """
        Export MFCC features ke CSV untuk VB.NET
        
        Args:
            mfcc (numpy.ndarray): MFCC features
            delta (numpy.ndarray): Delta MFCC
            delta2 (numpy.ndarray): Delta-delta MFCC
            stats (dict): Statistik MFCC
            output_path (str): Path untuk menyimpan CSV
        """
        output_path = Path(output_path)
        
        # Export MFCC mean (aggregated features) - cocok untuk ML
        csv_path = output_path.parent / f"{output_path.stem}_features.csv"
        with open(csv_path, 'w', newline='') as f:
            writer = csv.writer(f)
            
            # Header
            headers = []
            for i in range(self.n_mfcc):
                headers.extend([
                    f'mfcc_{i+1}_mean',
                    f'mfcc_{i+1}_std',
                    f'mfcc_{i+1}_min',
                    f'mfcc_{i+1}_max'
                ])
            writer.writerow(headers)
            
            # Data row
            row = []
            for i in range(self.n_mfcc):
                row.extend([
                    stats['mean'][i],
                    stats['std'][i],
                    stats['min'][i],
                    stats['max'][i]
                ])
            writer.writerow(row)
        
        # Export MFCC full matrix (untuk analisis detail)
        csv_full_path = output_path.parent / f"{output_path.stem}_mfcc_full.csv"
        np.savetxt(csv_full_path, mfcc, delimiter=',', fmt='%.6f')
        
        return csv_path, csv_full_path
    
    def export_to_vbnet_format(self, mfcc, delta, delta2, stats, output_path, audio_info):
        """
        Export ke format text yang mudah dibaca VB.NET
        
        Args:
            mfcc (numpy.ndarray): MFCC features
            delta (numpy.ndarray): Delta MFCC
            delta2 (numpy.ndarray): Delta-delta MFCC
            stats (dict): Statistik MFCC
            output_path (str): Path untuk menyimpan file
            audio_info (dict): Informasi audio (duration, sample_rate, dll)
        """
        output_path = Path(output_path)
        txt_path = output_path.parent / f"{output_path.stem}_vbnet.txt"
        
        with open(txt_path, 'w') as f:
            f.write("=" * 60 + "\n")
            f.write("MFCC FEATURES - VB.NET FORMAT\n")
            f.write("=" * 60 + "\n\n")
            
            # Audio Info
            f.write("[AUDIO_INFO]\n")
            f.write(f"FileName={audio_info.get('filename', 'unknown')}\n")
            f.write(f"Duration={audio_info.get('duration', 0):.3f}\n")
            f.write(f"SampleRate={audio_info.get('sample_rate', 0)}\n")
            f.write(f"MFCCShape={mfcc.shape[0]}x{mfcc.shape[1]}\n")
            f.write("\n")
            
            # MFCC Statistics (untuk feature vector)
            f.write("[MFCC_STATISTICS]\n")
            f.write("# Format: CoeffIndex,Mean,Std,Min,Max\n")
            for i in range(len(stats['mean'])):
                f.write(f"{i+1},{stats['mean'][i]:.6f},{stats['std'][i]:.6f},")
                f.write(f"{stats['min'][i]:.6f},{stats['max'][i]:.6f}\n")
            f.write("\n")
            
            # Feature Vector (single row untuk ML)
            f.write("[FEATURE_VECTOR]\n")
            f.write("# Aggregated features untuk machine learning\n")
            feature_vector = []
            for i in range(len(stats['mean'])):
                feature_vector.extend([
                    stats['mean'][i],
                    stats['std'][i],
                    stats['min'][i],
                    stats['max'][i]
                ])
            f.write(",".join([f"{v:.6f}" for v in feature_vector]) + "\n")
            f.write(f"# Total features: {len(feature_vector)}\n")
            f.write("\n")
            
            # MFCC Mean per coefficient (simplified)
            f.write("[MFCC_MEAN_VALUES]\n")
            f.write(",".join([f"{v:.6f}" for v in stats['mean']]) + "\n")
            f.write("\n")
            
        return txt_path
    
    def process_single_file(self, wav_path, output_dir=None, save_visualization=False):
        """
        Process single WAV file dan simpan hasilnya
        
        Args:
            wav_path (str): Path ke file WAV
            output_dir (str, optional): Directory untuk menyimpan output
            save_visualization (bool): Apakah menyimpan visualisasi
            
        Returns:
            dict: Dictionary berisi semua informasi MFCC
        """
        wav_path = Path(wav_path)
        
        if not wav_path.exists():
            raise FileNotFoundError(f"File tidak ditemukan: {wav_path}")
        
        print(f"Processing: {wav_path.name}")
        
        # Ekstrak MFCC dengan delta
        features = self.extract_mfcc_with_delta(str(wav_path))
        
        # Hitung statistik
        stats = self.get_mfcc_statistics(features['mfcc'])
        
        # Siapkan output directory
        if output_dir:
            output_dir = Path(output_dir)
            output_dir.mkdir(parents=True, exist_ok=True)
            
            # Simpan MFCC sebagai numpy file
            base_name = wav_path.stem
            np.save(output_dir / f"{base_name}_mfcc.npy", features['mfcc'])
            np.save(output_dir / f"{base_name}_delta.npy", features['delta'])
            np.save(output_dir / f"{base_name}_delta2.npy", features['delta_delta'])
            
            # Simpan statistik sebagai JSON
            stats_serializable = {k: v.tolist() for k, v in stats.items()}
            with open(output_dir / f"{base_name}_stats.json", 'w') as f:
                json.dump(stats_serializable, f, indent=2)
            
            # Export ke CSV (untuk VB.NET)
            audio_info = {
                'filename': wav_path.name,
                'duration': len(features['audio']) / features['sr'],
                'sample_rate': features['sr']
            }
            csv_path, csv_full_path = self.export_to_csv(
                features['mfcc'],
                features['delta'],
                features['delta_delta'],
                stats,
                output_dir / base_name
            )
            
            # Export ke VB.NET text format
            txt_path = self.export_to_vbnet_format(
                features['mfcc'],
                features['delta'],
                features['delta_delta'],
                stats,
                output_dir / base_name,
                audio_info
            )
            
            # Simpan visualisasi jika diminta
            if save_visualization:
                self.visualize_mfcc(
                    features['mfcc'],
                    features['sr'],
                    save_path=output_dir / f"{base_name}_mfcc.png"
                )
            
            print(f"Output disimpan di: {output_dir}")
            print(f"  - CSV features: {csv_path.name}")
            print(f"  - VB.NET format: {txt_path.name}")
        
        return {
            'mfcc': features['mfcc'],
            'delta': features['delta'],
            'delta_delta': features['delta_delta'],
            'statistics': stats,
            'shape': features['mfcc'].shape,
            'duration': len(features['audio']) / features['sr']
        }
    
    def process_directory(self, input_dir, output_dir, save_visualization=False, recursive=True):
        """
        Process semua file WAV dalam directory
        
        Args:
            input_dir (str): Directory berisi file WAV
            output_dir (str): Directory untuk menyimpan output
            save_visualization (bool): Apakah menyimpan visualisasi
            recursive (bool): Cari file WAV di subdirectory juga
            
        Returns:
            dict: Summary dari processing
        """
        input_dir = Path(input_dir)
        output_dir = Path(output_dir)
        
        # Cari semua file WAV (termasuk di subdirectory jika recursive=True)
        if recursive:
            wav_files = list(input_dir.rglob("*.wav")) + list(input_dir.rglob("*.WAV"))
        else:
            wav_files = list(input_dir.glob("*.wav")) + list(input_dir.glob("*.WAV"))
        
        if not wav_files:
            print(f"Tidak ada file WAV ditemukan di: {input_dir}")
            return {}
        
        print(f"Ditemukan {len(wav_files)} file WAV")
        print("-" * 50)
        
        results = {}
        for wav_file in wav_files:
            try:
                # Pertahankan struktur folder relatif
                relative_path = wav_file.relative_to(input_dir)
                output_subdir = output_dir / relative_path.parent / wav_file.stem
                
                result = self.process_single_file(
                    wav_file,
                    output_subdir,
                    save_visualization
                )
                results[str(relative_path)] = result
                print(f"✓ Berhasil: {relative_path} - Shape: {result['shape']}, Duration: {result['duration']:.2f}s")
            except Exception as e:
                print(f"✗ Error pada {wav_file.name}: {str(e)}")
                results[str(relative_path)] = {'error': str(e)}
            print("-" * 50)
        
        # Simpan summary
        summary_path = output_dir / "processing_summary.json"
        summary = {
            'total_files': len(wav_files),
            'successful': sum(1 for r in results.values() if 'error' not in r),
            'failed': sum(1 for r in results.values() if 'error' in r),
            'files': {k: {'shape': str(v.get('shape', 'N/A')), 
                         'duration': v.get('duration', 'N/A'),
                         'error': v.get('error', None)} 
                     for k, v in results.items()}
        }
        
        with open(summary_path, 'w') as f:
            json.dump(summary, f, indent=2)
        
        print(f"\nSummary disimpan di: {summary_path}")
        print(f"Total: {summary['total_files']}, Berhasil: {summary['successful']}, Gagal: {summary['failed']}")
        
        return results


def main():
    """
    Main function - Otomatis process file WAV dari folder 'dataset'
    """
    # Dapatkan directory script saat ini
    script_dir = Path(__file__).parent
    
    # Setup paths
    input_directory = script_dir / "dataset"
    output_directory = script_dir / "output" / "mfcc_dataset"
    
    print("=" * 60)
    print("WAV TO MFCC CONVERTER")
    print("=" * 60)
    print(f"Input Directory : {input_directory}")
    print(f"Output Directory: {output_directory}")
    print("=" * 60)
    
    # Cek apakah folder dataset ada
    if not input_directory.exists():
        print(f"\n❌ ERROR: Folder 'dataset' tidak ditemukan!")
        print(f"   Silakan buat folder 'dataset' di: {script_dir}")
        print(f"   Dan masukkan file WAV Anda ke dalamnya.")
        return
    
    # Inisialisasi converter dengan parameter yang bisa disesuaikan
    print("\nParameter MFCC:")
    print("- n_mfcc     : 13 (jumlah koefisien)")
    print("- n_fft      : 2048 (FFT window)")
    print("- hop_length : 512")
    print("- sample_rate: 22050 Hz")
    print("-" * 60)
    
    converter = WavToMFCC(
        n_mfcc=13,      # Jumlah koefisien MFCC
        n_fft=2048,     # FFT window size
        hop_length=512, # Hop length
        sr=22050        # Sample rate
    )
    
    # Process semua file WAV di folder dataset
    print("\n🚀 Memulai processing...\n")
    
    try:
        results = converter.process_directory(
            str(input_directory),
            str(output_directory),
            save_visualization=True  # Simpan visualisasi MFCC
        )
        
        if results:
            print("\n" + "=" * 60)
            print("✅ PROCESSING SELESAI!")
            print("=" * 60)
            print(f"\nHasil disimpan di: {output_directory}")
            print("\nFile yang dihasilkan untuk setiap audio:")
            print("  - *_mfcc.npy       : MFCC features")
            print("  - *_delta.npy      : Delta MFCC")
            print("  - *_delta2.npy     : Delta-delta MFCC")
            print("  - *_stats.json     : Statistik (mean, std, min, max)")
            print("  - *_mfcc.png       : Visualisasi MFCC")
            print("  - processing_summary.json : Summary semua file")
        else:
            print("\n⚠️  Tidak ada file WAV yang diproses.")
            print(f"   Pastikan ada file .wav di folder: {input_directory}")
            
    except Exception as e:
        print(f"\n❌ ERROR: {str(e)}")
        import traceback
        traceback.print_exc()


if __name__ == "__main__":
    main()
