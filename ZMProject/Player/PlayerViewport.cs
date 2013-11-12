using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class PlayerViewport : GameComponent
    {
        public Camera firstPersonCamera;

        public PlayerViewport(Game game, Vector3 position, Vector3 rotation)
            :base(game)
        {
            firstPersonCamera = new Camera(game, position, rotation, 35.0f);
        }

        public override void Update(GameTime gameTime)
        {
            firstPersonCamera.Update(gameTime, Game);
            base.Update(gameTime);
        }
    }
}
