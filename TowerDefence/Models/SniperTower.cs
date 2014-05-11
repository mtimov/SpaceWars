using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class SniperTower : Tower
    {
        public SniperTower(int X, int Y, string bitmap, int Demage = 200, int ReloadTime = 15, int Range = 300,int Cost = 150, int Width = 40, int Height = 40)
            : base(X, Y, Demage, ReloadTime, Range,Cost, Width, Height, bitmap)
        {

        }
    }
}
