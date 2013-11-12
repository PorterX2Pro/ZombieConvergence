using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject.Display
{
    internal class MainMenu : DrawableDisplay
    {
        private bool isLoaded = false;
        private bool isLoad = false;
        private CurrentDisplay currentDisplay = CurrentDisplay.Menu;

        public void Load(GraphicsDevice device, ContentManager cManage)
        {
            if (isLoad == false)
            {
                isLoad = true;
                TextureMemory.AddTextureToMemory("menuBG", cManage.Load<Texture2D>("background/zombieBackgroundOther"));
                FontMemory.AddFontToMemory("menuOption", cManage.Load<SpriteFont>("fonts/MenuOption"));
                SoundMemory.AddSoundToMemory("menuST", cManage.LoadSong("soundtracks/menuTest.mp3"));
                SoundMemory.MemorySound["menuST"].SetVolume(0.08f);
                //SoundMemory.MemorySound["menuST"].Play();
                isLoaded = true;
            }
        }

        public void Update(GameTime gameTime, GameWindow Window)
        {
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            if (isLoaded)
            {
                spriteBatch.Begin();
                switch (currentDisplay)
                {
                    case CurrentDisplay.Menu:
                        RenderMenu(ref device, ref spriteBatch);
                        break;
                }
                spriteBatch.End();
            }
        }

        private void RenderMenu(ref GraphicsDevice device, ref SpriteBatch spriteBatch)
        {
            //Draw Background Image
            spriteBatch.Draw(TextureMemory.MemoryTextures["menuBG"], new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height), Color.White);
            //Draw rectangle behind menu logo
            DrawUtilities.DrawRectangle(spriteBatch, new Rectangle((device.Viewport.Width - 540) / 2, 0, 540, 76), DrawUtilities.GetColorWithARGB(79, 79, 79, 240));
            //Draw menu logo
            spriteBatch.DrawString(FontMemory.MemoryFonts["menuOption"], "Zombie Convergence", new Vector2((device.Viewport.Width - FontMemory.MemoryFonts["menuOption"].MeasureString("Zombie Convergence").X) / 2, 5), Color.Red);
            //Draw rectangle in center
            DrawUtilities.DrawRectangle(spriteBatch, new Rectangle((device.Viewport.Width - 345) / 2, 120, 345, 260), DrawUtilities.GetColorWithARGB(79, 79, 79, 240));
            //Draw play text
            spriteBatch.DrawString(FontMemory.MemoryFonts["menuOption"], "Play", new Vector2((device.Viewport.Width - FontMemory.MemoryFonts["menuOption"].MeasureString("Play").X) / 2, 125), Color.Red);
            //Draw maps text
            spriteBatch.DrawString(FontMemory.MemoryFonts["menuOption"], "Maps", new Vector2((device.Viewport.Width - FontMemory.MemoryFonts["menuOption"].MeasureString("Maps").X) / 2, 178), Color.Red);
            //Draw friends text
            spriteBatch.DrawString(FontMemory.MemoryFonts["menuOption"], "Friends", new Vector2((device.Viewport.Width - FontMemory.MemoryFonts["menuOption"].MeasureString("Friends").X) / 2, 234), Color.Red);
            //Draw settings text
            spriteBatch.DrawString(FontMemory.MemoryFonts["menuOption"], "Options", new Vector2((device.Viewport.Width - FontMemory.MemoryFonts["menuOption"].MeasureString("Options").X) / 2, 294), Color.Red);
        }

        private enum CurrentDisplay
        {
            Menu,
            PlayMenu,
            MapMenu,
            FriendMenu,
            SettingsMenu
        }

        public bool contentLoaded
        {
            get
            {
                return this.isLoaded;
            }
            set
            {
                this.isLoaded = value;
            }
        }


        public bool isLoading
        {
            get { return isLoad; }
        }
    }
}
