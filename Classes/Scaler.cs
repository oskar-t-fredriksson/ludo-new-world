using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;

namespace LudoNewWorld
{
    class Scaler
    {

        /// <summary>
        /// Scale the content and canvas to the current window width and height
        /// </summary>
        public static void SetScale()
        {         
            MainPage.scaleWidth = (float)MainPage.bounds.Width / MainPage.DesignWidth;
            MainPage.scaleHeight = (float)MainPage.bounds.Height / MainPage.DesignHeight;
        }


        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns>a scaled <see cref="Transform2DEffect"/> image</returns>
        public static Transform2DEffect Img(CanvasBitmap source)
        {
            return ScaleImage(source, MainPage.scaleWidth, MainPage.scaleHeight);
        }


        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns>a scaled <see cref="Transform2DEffect"/> that fits to the window size ratio</returns>
        public static Transform2DEffect Fit(CanvasBitmap source)
        {
            float scale = MathF.Min((float)(MainPage.bounds.Width / source.Size.Width), (float)(MainPage.bounds.Height / source.Size.Height));
            return ScaleImage(source, scale, scale);
        }


        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns>a scaled <see cref="Transform2DEffect"/> image based on the window size ratio</returns>
        private static Transform2DEffect ScaleImage(CanvasBitmap source, float scaleX, float scaleY)
        {
            Transform2DEffect image;
            image = new Transform2DEffect() { Source = source };
            image.TransformMatrix = Matrix3x2.CreateScale(MainPage.scaleWidth, MainPage.scaleHeight);
            return image;
        }


        /// <summary>
        /// Scales the X and Y coordinates in the Vector2 value of each object based on a ratio of the window size
        /// </summary>
        /// <param name="cords"></param>
        /// <returns>a <see cref="Vector2"/> scaled by window size ratio</returns>
        public static Vector2 Cords(Vector2 cords)
        {
            var scaleRatioWidth = (float)MainPage.bounds.Width / (MainPage.DesignWidth + 50);
            var scaleRatioheight = (float)MainPage.bounds.Height / (MainPage.DesignHeight + 50);
            return new Vector2(cords.X * scaleRatioWidth, cords.Y * scaleRatioheight);
        }


        /// <summary>
        /// Scales the X and Y coordinates in the Vector2 value of each object based on a ratio of the window size
        /// </summary>
        /// <param name="cords"></param>
        /// <returns>a <see cref="Vector2"/> scaled by window size ratio</returns>
        public static Vector2 TileCords(Vector2 cords)
        {
            var scaleRatioWidth = (float)MainPage.bounds.Width / (MainPage.DesignWidth + 25);
            var scaleRatioheight = (float)MainPage.bounds.Height / (MainPage.DesignHeight + 25);
            return new Vector2(cords.X * scaleRatioWidth, cords.Y * scaleRatioheight);
        }


        /// <summary>
        /// Scales the X and Y coordinates from a user pressing on the screen with their mouse based on a ratio of the window size
        /// </summary>
        /// <param name="cords"></param>
        /// <returns>a <see cref="Vector2"/> scaled by window size ratio</returns>
        public static Vector2 ClickCords(Vector2 cords)
        {
            var scaleRatioWidth = (float)MainPage.bounds.Width / (MainPage.DesignWidth);
            var scaleRatioheight = (float)MainPage.bounds.Height / (MainPage.DesignHeight);

            return new Vector2(cords.X - 32 * scaleRatioWidth, cords.Y - 45 * scaleRatioheight);
        }
    }
}
