using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ZMProject.Display
{
    internal class DisplayManager
    {
        private static List<DrawableDisplay> DisplayStack;

        public static void Initialize()
        {
            DisplayStack = new List<DrawableDisplay>();
        }

        public static bool SetCurrentDisplayMoveOtherBack(DrawableDisplay Set)
        {
            if (DisplayStack.Count == 2)
            {
                return false;
            }
            DisplayStack.Add(Set);
            if (DisplayStack.Count > 1)
            {
                DrawableDisplay Temp = DisplayStack[0];
                DisplayStack[0] = DisplayStack[1];
                DisplayStack[1] = Temp;
            }
            return true;
        }

        public static void SetCurrentDisplayRemoveOther(DrawableDisplay Set)
        {
            DisplayStack.Clear();
            DisplayStack.Add(Set);
        }

        public static void RemoveADisplay(int Display)
        {
            DisplayStack.RemoveAt(Display);
        }

        public static void LoadDisplays(GraphicsDevice device, ContentManager cManage)
        {
            Thread loadThread = new Thread(new ParameterizedThreadStart(LoadDisplaysAsync));
            loadThread.Start(new DisplayAsyncPass(device, cManage));
        }

        private static void LoadDisplaysAsync(object LoadInfo)
        {
            DisplayAsyncPass pass = (DisplayAsyncPass)LoadInfo;
            if (DisplayStack.Count == 1)
            {
                if (DisplayStack[0].isLoading == false && DisplayStack[0].contentLoaded == false)
                {
                    DisplayStack[0].Load(pass.device, pass.cManage);
                }
            }
            else if (DisplayStack.Count == 2)
            {
                if (DisplayStack[1].isLoading == false && DisplayStack[1].contentLoaded == false)
                {
                    DisplayStack[1].Load(pass.device, pass.cManage);
                }
                if (DisplayStack[0].isLoading == false && DisplayStack[0].contentLoaded == false)
                {
                    DisplayStack[0].Load(pass.device, pass.cManage);
                }
            }
        }

        public static void DrawDisplays(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            if (DisplayStack.Count == 1)
            {
                DisplayStack[0].Draw(device, spriteBatch);
            }
            else if (DisplayStack.Count == 2)
            {
                DisplayStack[1].Draw(device, spriteBatch);
                DisplayStack[0].Draw(device, spriteBatch);
            }
        }

        public static bool DisplaysNeedLoad()
        {
            if (DisplayStack.Count == 1)
            {
                if (DisplayStack[0].contentLoaded == false && DisplayStack[0].isLoading == false)
                {
                    return true;
                }
            }
            else if (DisplayStack.Count == 2)
            {
                foreach (DrawableDisplay display in DisplayStack)
                {
                    if (display.contentLoaded == false && display.isLoading == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void UpdateDisplays(GameTime gameTime, GameWindow Window)
        {
            //Only update main drawing on MAIN thread
            if (DisplayStack.Count > 0)
            {
                DisplayStack[0].Update(gameTime, Window);
            }
        }
    }
}
