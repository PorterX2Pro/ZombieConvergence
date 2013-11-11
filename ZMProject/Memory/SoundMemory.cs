using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class SoundMemory
    {
        public static Dictionary<string, Sound.Song> MemorySound;
        /// <summary>
        /// Initialize Sound Memory
        /// </summary>
        public static void Init()
        {
            MemorySound = new Dictionary<string, Sound.Song>();
        }
        /// <summary>
        /// Adds a song to sound memory
        /// </summary>
        /// <param name="Sound">Song Name</param>
        /// <param name="Song">Song</param>
        public static void AddSoundToMemory(string Sound, Sound.Song Song)
        {
            MemorySound.Add(Sound, Song);
        }
        /// <summary>
        /// Clears songs from sound memory
        /// </summary>
        public static void ClearSoundMemory()
        {
            foreach (KeyValuePair<string, Sound.Song> pair in MemorySound)
            {
                pair.Value.Dispose();
            }
        }
    }
}
