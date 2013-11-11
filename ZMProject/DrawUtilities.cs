using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class DrawUtilities
    {
        public static Color GetColorWithAlpha(Color color, byte Alpha)
        {
            color.A = Alpha;
            return color;
        }

        public static Color GetColorWithARGB(byte R, byte G, byte B, byte A)
        {
            Color color = Color.White;
            color.A = A;
            color.R = R;
            color.G = G;
            color.B = B;
            return color;
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle coords, Color color)
        {
            spriteBatch.Draw(HUDMemory.MemoryHUD[color], coords, color);
        }
    }
}
