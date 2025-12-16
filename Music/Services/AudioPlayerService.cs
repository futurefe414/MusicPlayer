using System;
using System.IO;
using NAudio.Wave;

namespace Music.Services
{
    public class AudioPlayerService : IDisposable
    {
        private IWavePlayer? _outputDevice;
        private AudioFileReader? _audioFile;
        private string? _currentFilePath;

        public event EventHandler? PlaybackStopped;
        public bool IsPlaying => _outputDevice?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => _outputDevice?.PlaybackState == PlaybackState.Paused;

        public void Play(string filePath)
        {
            try
            {
                if (_currentFilePath != filePath)
                {
                    Stop();
                    
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException($"Audio file not found: {filePath}");
                    }

                    _audioFile = new AudioFileReader(filePath);
                    _outputDevice = new WaveOutEvent();
                    _outputDevice.Init(_audioFile);
                    _outputDevice.PlaybackStopped += OnPlaybackStopped;
                    _currentFilePath = filePath;
                }

                _outputDevice?.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
                throw;
            }
        }

        public void Pause()
        {
            _outputDevice?.Pause();
        }

        public void Resume()
        {
            if (IsPaused)
            {
                _outputDevice?.Play();
            }
        }

        public void Stop()
        {
            _outputDevice?.Stop();
            _audioFile?.Dispose();
            _outputDevice?.Dispose();
            _audioFile = null;
            _outputDevice = null;
            _currentFilePath = null;
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
