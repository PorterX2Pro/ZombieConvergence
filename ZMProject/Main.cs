#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace ZMProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Main()
            : base()
        {
            global.Setup(this);
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 10;
            this.Window.Title = "Zombie Convergence";
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 720;
            this.IsMouseVisible = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 10;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialize Texture Memory
            TextureMemory.Init();
            //Initialize Font Memory
            FontMemory.Init();
            //Initialize HUD Memory
            HUDMemory.Init();
            //Initialize Sound Memory
            SoundMemory.Init();
            //Initialize Display
            Display.DisplayManager.Initialize();
            //Add a level
            this.Components.Add(new PlayerViewport(this, new Vector3(0, 25, 0), Vector3.Forward));
            Level CurrentLevel = LevelLoad.LoadLevelFromLvl("desert.lvl");
            Display.DisplayManager.SetCurrentDisplayMoveOtherBack(CurrentLevel);
            ////Add Main Menu
            //Display.DisplayManager.SetCurrentDisplayMoveOtherBack(new Display.MainMenu());
            //Center form to screen
            this.Window.SetPosition(new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 1200) / 2, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 720) / 2));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Precatch default fonts
            FontMemory.Precatch(Content);
            HUDMemory.GenerateARGBTextureToMemory(GraphicsDevice, 79, 79, 79, 240);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //Unload all content loaded in memory
            TextureMemory.ClearTextureMemory();
            FontMemory.ClearFontMemory();
            HUDMemory.ClearHUDMemory();
            SoundMemory.ClearSoundMemory();
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Debug game exit TO BE REMOVED
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            //Update displays if no load is needed
            if (Display.DisplayManager.DisplaysNeedLoad())
            {
                //Load Needed
                Display.DisplayManager.LoadDisplays(GraphicsDevice, Content);
            }
            else
            {
                Display.DisplayManager.UpdateDisplays(gameTime, Window);
            }
            //End all Update code
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Draw Displays
            Display.DisplayManager.DrawDisplays(GraphicsDevice, spriteBatch);
            //End Drawing
            base.Draw(gameTime);
        }
    }
}
