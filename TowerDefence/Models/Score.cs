using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    [Serializable]
    public class Score : IComparable<Score>
    {
        string player;
        int score;

        public Score(string player, int score)
        {
            this.player = player;
            this.score = score;
        }

        public override string ToString()
        {
            return string.Format("{0,-10}  {1,-10}",player,score);
        }



        public int CompareTo(Score other)
        {
            return other.score - this.score;
        }
    }
}
