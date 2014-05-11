using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class RapidFireTower : Tower
    {
        public RapidFireTower(int X, int Y, string bitmap, int Demage = 25, int ReloadTime = 5, int Range = 100,int Cost = 25, int Width = 40, int Height = 40)
            : base(X, Y, Demage, ReloadTime, Range,Cost, Width, Height, bitmap)
        {

        }
    }
}
