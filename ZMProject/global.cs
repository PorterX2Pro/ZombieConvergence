using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class global
    {
        private static Game CurrentGame;

        public static void Setup(Game game)
        {
            CurrentGame = game;
        }

        public static Camera GetFirstPersonCamera()
        {
            return ((PlayerViewport)CurrentGame.Components[0]).firstPersonCamera;
        }
    }
}
