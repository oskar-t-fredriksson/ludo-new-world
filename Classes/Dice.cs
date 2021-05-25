using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI.Xaml.Controls;

namespace LudoNewWorld.Classes
{
    class Dice
    {
        public static Timer timer;
        public static List<CanvasBitmap> Dicepictures=new List<CanvasBitmap>();  
        public static CanvasBitmap[] pics;
        public static  int diceValue;
        public static CanvasBitmap dice;
        public static Random random=new Random();
        public Dice()
        {

        }

         public static int DiceRoll()
        {
           
            
                diceValue = random.Next(1, 7);
                  return diceValue;
            
        }
      
      





    }
}
