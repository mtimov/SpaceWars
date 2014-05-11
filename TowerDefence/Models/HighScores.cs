using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class HighScores
    {
        List<Score> scores;
        private string path;
        public HighScores()
        {
            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\HighScores\" + "score.hscr");
            LoadScores();
        }

        //loads scores from file if it exists or creates an empty list otherwise 
        private void LoadScores()
        {
            if (!File.Exists(path))
            {
                scores = new List<Score>();
                return;
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    IFormatter formater = new BinaryFormatter();
                    scores = (List<Score>)formater.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine("Could not read file: " + path);
                
                return;
            }

        }

        //adds score and saves it to file
        public void addScore(Score score)
        {
            scores.Add(score);
            scores.Sort();
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, scores);
            }
        }

        //returns top 5 scores
        public override string ToString()
        {
            if (scores.Count == 0)
            {
                return "No scores available";
            }
            
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (Score s in scores)
            {
                if (count > 5)
                    break;
                sb.Append(count);
                sb.Append(". ");
                sb.Append(s.ToString());
                sb.Append("\n");
                count++;
            }
            return sb.ToString();
        }
    }
}
