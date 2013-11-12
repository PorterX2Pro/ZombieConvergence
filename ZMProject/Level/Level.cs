using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class Level : Display.DrawableDisplay
    {
        /// <summary>
        /// The name of the level specified in the mod.
        /// </summary>
        public string LevelName;
        /// <summary>
        /// A list of entities contained in the level
        /// </summary>
        public List<LevelEntity> Entities;

        //View and Projection Matrix
        private Matrix view;
        private Matrix projection;

        //Camera position and Angles
        private Vector3 cameraPosition = Vector3.Zero;
        private Vector3 cameraAngle = Vector3.Zero;
        private Vector3 cameraForward = Vector3.Forward;
        private Vector3 cameraLeft = Vector3.Left;
        //a list of meshes ready to draw
        private List<VertexMesh> Meshes;
        //Is content loaded
        private bool isLoaded = false;
        private bool isLoad = false;
        //The effect used to apply textures, lighting etc.
        private BasicEffect drawEffect;

        public Level()
        {
            Entities = new List<LevelEntity>();
            Meshes = new List<VertexMesh>();
        }

        /// <summary>
        /// Load and Initialize the level setting defaults and loading meshes.
        /// </summary>
        /// <param name="device">The graphics device</param>
        /// <param name="cManage">The content manager</param>
        public void Load(GraphicsDevice device, ContentManager cManage)
        {
            isLoad = true;
            //The viewport modifier, allows scale
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 4.0f / 3.0f, 0.1f, 10000.0f);
            //The position of the camera (or head of player)
            cameraPosition = new Vector3(0, 10, 0);
            foreach (LevelMesh mesh in Entities[0].Meshes)
            {
                TextureMemory.LoadTextureIntoMemory(mesh.Material, cManage);
                Meshes.Add(new VertexMesh(device, mesh.Verts, mesh.Position, mesh.Material));
            }
            drawEffect = new BasicEffect(device);
            isLoaded = true;
        }
        /// <summary>
        /// Updates various entities in the game in real time
        /// </summary>
        /// <param name="gameTime">A reference to the gameTime</param>
        /// <param name="Window">A reference to the Window</param>
        public void Update(GameTime gameTime, GameWindow Window)
        {
            if (contentLoaded)
            {
                //const float speed = 200.0f;
                //const float rotSpeed = 20.0f;

                //KeyboardState keyboardState = Keyboard.GetState();
                //MouseState mouseState = Mouse.GetState();

                //cameraAngle.X += MathHelper.ToRadians((mouseState.Y - Window.ClientBounds.Height / 2) * rotSpeed * 0.01f); // pitch
                //cameraAngle.Y += MathHelper.ToRadians((mouseState.X - Window.ClientBounds.Width / 2) * rotSpeed * 0.01f); // yaw

                //cameraForward = Vector3.Normalize(new Vector3((float)Math.Sin(-cameraAngle.Y), (float)Math.Sin(cameraAngle.X), (float)Math.Cos(-cameraAngle.Y)));
                //cameraLeft = Vector3.Normalize(new Vector3((float)Math.Cos(cameraAngle.Y), 0f, (float)Math.Sin(cameraAngle.Y)));

                //if (keyboardState.IsKeyDown(Keys.W))
                //    cameraPosition -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds * cameraForward;
                //if (keyboardState.IsKeyDown(Keys.S))
                //    cameraPosition += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * cameraForward;

                //if (keyboardState.IsKeyDown(Keys.A))
                //    cameraPosition -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds * cameraLeft;
                //if (keyboardState.IsKeyDown(Keys.D))
                //    cameraPosition += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * cameraLeft;

                //if (keyboardState.IsKeyDown(Keys.LeftShift))
                //    cameraPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if (keyboardState.IsKeyDown(Keys.LeftControl))
                //    cameraPosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //view = Matrix.Identity;
                //view *= Matrix.CreateTranslation(-cameraPosition);
                //view *= Matrix.CreateRotationY(cameraAngle.Y);
                //view *= Matrix.CreateRotationX(cameraAngle.X);
                //Mouse.SetPosition(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            }
        }
        /// <summary>
        /// Renders out level onto the graphics device
        /// </summary>
        /// <param name="device">A reference to the device</param>
        /// <param name="sp">A reference to the spriteBatch</param>
        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            if (contentLoaded)
            {
                //Resets Depth (When drawing with SpriteBatch[2d] it changes it and makes 3d malfunction)
                device.BlendState = BlendState.Opaque;
                device.RasterizerState = RasterizerState.CullCounterClockwise;
                device.DepthStencilState = DepthStencilState.Default;
                drawEffect.View = global.GetFirstPersonCamera().View;
                drawEffect.Projection = global.GetFirstPersonCamera().Projection;
                drawEffect.World = Matrix.Identity;
                drawEffect.TextureEnabled = true;
                foreach (VertexMesh vmesh in Meshes)
                {
                    drawEffect.Texture = TextureMemory.MemoryTextures[vmesh.Material];
                    drawEffect.CurrentTechnique.Passes[0].Apply();
                    device.SetVertexBuffer(vmesh.vBuffer);
                    device.DrawPrimitives(PrimitiveType.TriangleList, 0, vmesh.vBuffer.VertexCount);
                }
            }
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
