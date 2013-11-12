using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal static class ExtensionMethods
    {
        public static Sound.Song LoadSong(this ContentManager content, string File)
        {
            Sound.Song toReturn = new Sound.Song();
            toReturn.fileStream = System.IO.File.OpenRead("Content/" + File);
            toReturn.fileReader = new NAudio.Wave.Mp3FileReader(toReturn.fileStream);
            toReturn.waveStream = WaveFormatConversionStream.CreatePcmStream(toReturn.fileReader);
            toReturn.baReductionStream = new BlockAlignReductionStream(toReturn.waveStream);
            toReturn.waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            toReturn.waveOut.Init(toReturn.baReductionStream);
            return toReturn;
        }

        public static void SetPosition(this GameWindow window, Point position)
        {
            OpenTK.GameWindow OTKWindow = GetForm(window);
            if (OTKWindow != null)
            {
                OTKWindow.X = position.X;
                OTKWindow.Y = position.Y;
            }
        }

        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
        {
            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
                return field.GetValue(gameWindow) as OpenTK.GameWindow;
            return null;
        }
    }
}
