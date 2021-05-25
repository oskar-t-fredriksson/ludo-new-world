using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LudoNewWorld.Classes
{
    class Dice
    {
        
        public static  int diceValue;
        public static CanvasBitmap dice;
        public static Random random=new Random();
         public static int DiceRoll()
        {
           diceValue= random.Next(1, 7);
            return diceValue;
        }
      
      





    }
}
