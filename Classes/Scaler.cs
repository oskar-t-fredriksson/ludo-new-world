using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LudoNewWorld.Classes;
using System.Diagnostics;
using Windows.UI.Xaml;

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

        // Method to scale the x and y coordinates in the Vector2 value of each object based on a ratio of the windowsize
        public static Vector2 Cords(Vector2 cords)
        {
            var scaleRatioWidth = (float)MainPage.bounds.Width / (MainPage.DesignWidth + 50);
            var scaleRatioheight = (float)MainPage.bounds.Height / (MainPage.DesignHeight + 50);
            return new Vector2(cords.X * scaleRatioWidth, cords.Y * scaleRatioheight);
        }

        public static Vector2 TileCords(Vector2 cords)
        {
            var scaleRatioWidth = (float)MainPage.bounds.Width / (MainPage.DesignWidth + 25);
            var scaleRatioheight = (float)MainPage.bounds.Height / (MainPage.DesignHeight + 25);
            return new Vector2(cords.X * scaleRatioWidth, cords.Y * scaleRatioheight);
        }
        public static Vector2 ClickCords(Vector2 cords)
        {
            var scaleRatioWidth = (float)MainPage.bounds.Width / (MainPage.DesignWidth);
            var scaleRatioheight = (float)MainPage.bounds.Height / (MainPage.DesignHeight);

            return new Vector2(cords.X - 32 * scaleRatioWidth, cords.Y - 45 * scaleRatioheight);
        }
    }
}
