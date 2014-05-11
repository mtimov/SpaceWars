using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Wave
    {
        List<BasicEnemy> basicEnemies;
        List<RunnerEnemy> runnerEnemies;
        List<BigBossEnemy> bigBossEnemies;

        public Wave(int numBasics, int numRunners, int numBosses, int waveNum)
        {
            basicEnemies = new List<BasicEnemy>();
            runnerEnemies = new List<RunnerEnemy>();
            bigBossEnemies = new List<BigBossEnemy>();
            
            //Generating Basic enemies
            var bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "test.png");
            for (int i = 1; i <= numBasics; i++)
            {
                basicEnemies.Add(new BasicEnemy(-25 * i, 200, 200 + waveNum * 100, bitmap: bitmap));
            }

            //Generating BigBoss enemies
            bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "test1.png");
            for (int i = 1; i <= numBosses; i++)
            {
                bigBossEnemies.Add(new BigBossEnemy(-55 * i - (numBasics * 25) - (numRunners * 5), 200, 10000 + waveNum * 1000, bitmap: bitmap));
            }

            //Generating Runner enemies
            bitmap = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\Images\" + "test2.png");
            for (int i = 1; i <= numRunners; i++)
            {
                runnerEnemies.Add(new RunnerEnemy(-25 * i - (numBasics * 25), 150, 250 + waveNum * 100, bitmap: bitmap));
            }

        }

        //returns enemies meant for Fast lane
        public List<Enemy> getFastEnemies()
        {
            List<Enemy> list = new List<Enemy>();

            foreach (Enemy enemy in runnerEnemies)
            {
                list.Add(enemy);
            }

            return list;
        }

        //returns enemies meant for Slow lane
        public List<Enemy> getSlowEnemies()
        {
            List<Enemy> list = new List<Enemy>();

            foreach (Enemy enemy in basicEnemies)
            {
                list.Add(enemy);
            }
            foreach (Enemy enemy in bigBossEnemies)
            {
                list.Add(enemy);
            }

            return list;
        }

    }
}
