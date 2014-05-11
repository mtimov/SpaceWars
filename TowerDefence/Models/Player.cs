using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Player
    {
        public string Name { get; set; }
        public int Coins { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public Player(string Name)
        {
            this.Name = Name;
            this.Coins = 10;
            this.Score = 0;
            this.Lives = 20;
        }

        public void GotKill(int reward)
        {
            this.Coins += reward;
            this.Score += reward * 15;
        }

        public void LostLife()
        {
            this.Lives--;
        }
        public bool IsGameOver() 
        {
            return (Lives > 0);
        }
        public void BuiltTower(int cost)
        {
            this.Coins -= cost;
        }
    }
}
