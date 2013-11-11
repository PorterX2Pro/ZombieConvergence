using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class TextureMemory
    {
        /// <summary>
        /// A list of textures stored in memory
        /// </summary>
        public static Dictionary<string, Texture2D> MemoryTextures;
        /// <summary>
        /// Initializes the texture memory
        /// </summary>
        public static void Init()
        {
            MemoryTextures = new Dictionary<string, Texture2D>();
        }
        /// <summary>
        /// Adds a texture to memory
        /// </summary>
        /// <param name="Material">Texture name</param>
        /// <param name="Texture">Texture to add</param>
        public static void AddTextureToMemory(string Material, Texture2D Texture)
        {
            if (!MemoryTextures.ContainsKey(Material))
            {
                MemoryTextures.Add(Material, Texture);
            }
        }
        /// <summary>
        /// Clears the textures loaded in the memory
        /// </summary>
        public static void ClearTextureMemory()
        {
            MemoryTextures.Clear();
        }
        /// <summary>
        /// Load a texture into the memory
        /// </summary>
        /// <param name="Material">Texture name</param>
        /// <param name="Manager">Content Manager</param>
        public static void LoadTextureIntoMemory(string Material, ContentManager Manager)
        {
            if (!MemoryTextures.ContainsKey(Material))
            {
                MemoryTextures.Add(Material, Manager.Load<Texture2D>("textures\\" + Material));
            }
        }
    }
}
