using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class DestroyerTower : Tower
    {
        public DestroyerTower(int X, int Y, string bitmap, int Demage = 500, int ReloadTime = 5, int Range = 200,int Cost = 300, int Width = 40, int Height = 40)
            : base(X, Y, Demage, ReloadTime, Range,Cost, Width, Height, bitmap)
        {

        }
    }
}
