using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class RunnerEnemy : Enemy
    {
        public RunnerEnemy(int X, int Y, int Health = 250, string bitmap = "", int Reward = 10, int Speed = 3, int Width = 25, int Height = 25)
            : base(X, Y, Health, Speed,Reward, Width, Height, bitmap)
        {

        }
    }
}
