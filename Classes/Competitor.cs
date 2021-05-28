using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LudoNewWorld.Classes
{
    abstract class Competitor
    {
        public static Vector2 Card1 = new Vector2();
        public static Vector2 Card2 = new Vector2();
        public static Vector2 Card3 = new Vector2();
        public static Vector2 Card4 = new Vector2();
        public static bool ISMyTurn;
        public static int RowBoatOut;
        public Competitor(Vector2 card1, Vector2 card2, Vector2 card3, Vector2 card4, bool IsMyTurn)
        {
            Card1 = card1;
            Card2 = card2;
            Card3 = card3;
            Card4 = card4;
            ISMyTurn = IsMyTurn;
            RowBoatOut = 4;
        }
        public  virtual void RollDicee()
        {
            if (ISMyTurn==true)
            {
                 GraphicHandler.scrambleDice(1);
            }
            else
            {
                //change the visibilty to collapse
               
            }

        }
        public  void MoveRowBoat() 
        {
        
                    
        
        }

    }
}