using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace ZMProject.Display
{
    internal interface DrawableDisplay
    {
        void Load(GraphicsDevice device, ContentManager cManage);
        void Update(GameTime gameTime, GameWindow Window);
        void Draw(GraphicsDevice device, SpriteBatch spriteBatch);
        bool contentLoaded { get; set; }
        bool isLoading { get; }
    }
}
