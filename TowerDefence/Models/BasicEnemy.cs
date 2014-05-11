using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy(int X, int Y, int Health = 200, string bitmap = "", int Reward = 2, int Speed = 2, int Width = 25, int Height = 25)
            : base(X, Y, Health, Speed,Reward, Width, Height, bitmap)
        {

        }
    }
}
