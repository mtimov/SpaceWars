using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class BigBossEnemy : Enemy
    {
        public BigBossEnemy(int X, int Y,int Health = 10000, string bitmap = "",int Reward = 100,  int Speed = 2, int Width = 50, int Height = 50)
            : base(X, Y, Health, Speed,Reward, Width, Height, bitmap)
        {

        }
    }
}
