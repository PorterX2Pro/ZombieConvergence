using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class FontMemory
    {
        /// <summary>
        /// A list of fonts stored im memory
        /// </summary>
        public static Dictionary<string, SpriteFont> MemoryFonts;
        /// <summary>
        /// Initializes the font memory
        /// </summary>
        public static void Init()
        {
            MemoryFonts = new Dictionary<string, SpriteFont>();
        }
        /// <summary>
        /// Loads the default fonts
        /// </summary>
        /// <param name="content">Content Manager</param>
        public static void Precatch(ContentManager content)
        {
            MemoryFonts.Add("opensans10", content.Load<SpriteFont>("fonts/OpenSans10"));
            MemoryFonts.Add("opensans12", content.Load<SpriteFont>("fonts/OpenSans12"));
            MemoryFonts.Add("console", content.Load<SpriteFont>("fonts/Console"));
        }

        /// <summary>
        /// Adds a font into memory fonts
        /// </summary>
        /// <param name="Font">The name of the Font</param>
        /// <param name="toAdd">The font itself</param>
        public static void AddFontToMemory(string Font, SpriteFont toAdd)
        {
            if (!MemoryFonts.ContainsKey(Font))
            {
                MemoryFonts.Add(Font, toAdd);
            }
        }
        /// <summary>
        /// Clears the font memory
        /// </summary>
        public static void ClearFontMemory()
        {
            MemoryFonts.Clear();
        }
        /// <summary>
        /// Loads a font into memory
        /// </summary>
        /// <param name="Font">Name of font to load</param>
        /// <param name="Manage">Content Manager</param>
        public static void LoadFontIntoMemory(string Font, ContentManager Manage)
        {
            if (!MemoryFonts.ContainsKey(Font))
            {
                MemoryFonts.Add(Font, Manage.Load<SpriteFont>(Font));
            }
        }
    }
}
