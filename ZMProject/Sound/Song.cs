using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace ZMProject.Sound
{
    internal class Song : IDisposable
    {
        //Required Streams for song manipulation
        public FileStream fileStream;
        public Mp3FileReader fileReader;
        public WaveStream waveStream;
        public BlockAlignReductionStream baReductionStream;
        public WaveOut waveOut;
        public PlaybackState PlayState = PlaybackState.Stopped;
        /// <summary>
        /// Initialize Song
        /// </summary>
        public void Init()
        {
            waveOut.PlaybackStopped += waveOut_PlaybackStopped;
        }
        /// <summary>
        /// Play the song at current position
        /// </summary>
        public void Play()
        {
            if (PlayState == PlaybackState.Stopped)
            {
                PlayState = PlaybackState.Playing;
                waveOut.Play();
            }
            else
            {
                PlayState = PlaybackState.Playing;
                waveOut.Resume();
            }
        }
        /// <summary>
        /// Stop playing the song
        /// </summary>
        public void Stop()
        {
            PlayState = PlaybackState.Stopped;
            waveOut.Stop();
        }
        /// <summary>
        /// Pause the song
        /// </summary>
        public void Pause()
        {
            PlayState = PlaybackState.Paused;
            waveOut.Pause();
        }
        /// <summary>
        /// Set the volume of the player
        /// </summary>
        /// <param name="Value">0-1</param>
        public void SetVolume(float Value)
        {
            waveOut.Volume = Value;
        }

        void waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception == null)
            {
                PlayState = PlaybackState.Stopped;
            }
        }
        /// <summary>
        /// Free song file from memory
        /// </summary>
        public void Dispose()
        {
            waveOut.Stop();
            waveOut.Dispose();
            baReductionStream.Close();
            waveStream.Close();
            fileReader.Close();
            fileStream.Close();
        }
    }
}
