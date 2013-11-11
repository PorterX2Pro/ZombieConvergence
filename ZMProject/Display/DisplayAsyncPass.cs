using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject.Display
{
    internal class DisplayAsyncPass
    {
        public GraphicsDevice device;
        public ContentManager cManage;

        public DisplayAsyncPass(GraphicsDevice d, ContentManager c)
        {
            device = d;
            cManage = c;
        }
    }
}
