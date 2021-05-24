using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LudoNewWorld.Classes;

namespace LudoNewWorld
{
    class Scaler
    {
        public static void SetScale()
        {
            //Display information            
            MainPage.scaleWidth = (float)MainPage.bounds.Width / MainPage.DesignWidth;
            MainPage.scaleHeight = (float)MainPage.bounds.Height / MainPage.DesignHeight;
        }

        public static Transform2DEffect Img(CanvasBitmap source)
        {
            return ScaleImage(source, MainPage.scaleWidth, MainPage.scaleHeight);
        }

        public static Transform2DEffect Fit(CanvasBitmap source)
        {
            float scale = MathF.Min((float)(MainPage.bounds.Width / source.Size.Width), (float)(MainPage.bounds.Height / source.Size.Height));
            return ScaleImage(source, scale, scale);
        }

        private static Transform2DEffect ScaleImage(CanvasBitmap source, float scaleX, float scaleY)
        {
            Transform2DEffect image;
            image = new Transform2DEffect() { Source = source };
            image.TransformMatrix = Matrix3x2.CreateScale(MainPage.scaleWidth, MainPage.scaleHeight);
            return image;
        }
    }
}
