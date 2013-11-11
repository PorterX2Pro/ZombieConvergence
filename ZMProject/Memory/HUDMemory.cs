using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZMProject
{
    internal class HUDMemory
    {
        public static Dictionary<Color, Texture2D> MemoryHUD;

        public static void Init()
        {
            MemoryHUD = new Dictionary<Color, Texture2D>();
        }

        public static void GenerateColorTextureToMemory(GraphicsDevice device, Color color, byte Alpha)
        {
            color.A = Alpha;
            if (!MemoryHUD.ContainsKey(color))
            {
                Texture2D Texture = new Texture2D(device, 1, 1);
                Texture.SetData(new[] { color });
                MemoryHUD.Add(color, Texture);
            }
        }

        public static void GenerateARGBTextureToMemory(GraphicsDevice device, byte R, byte G, byte B, byte A)
        {
            Color color = Color.White;
            color.A = A;
            color.R = R;
            color.G = G;
            color.B = B;
            if (!MemoryHUD.ContainsKey(color))
            {
                Texture2D Texture = new Texture2D(device, 1, 1);
                Texture.SetData(new[] { color });
                MemoryHUD.Add(color, Texture);
            }
        }

        public static void ClearHUDMemory()
        {
            MemoryHUD.Clear();
        }
    }
}
