using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class SmasherTower : Tower
    {
        public SmasherTower(int X, int Y, string bitmap, int Demage = 80, int ReloadTime = 14, int Range = 100,int Cost = 65, int Width = 40, int Height = 40)
            : base(X, Y, Demage, ReloadTime, Range,Cost, Width, Height, bitmap)
        {

        }
    }
}
