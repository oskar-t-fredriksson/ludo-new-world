using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI.Xaml.Controls;
using LudoNewWorld.Classes;

namespace LudoNewWorld
{
    public class Dice
    {
        private readonly Random _random = new Random();
        public int Roll() => _random.Next(1, 7);
    }
}
