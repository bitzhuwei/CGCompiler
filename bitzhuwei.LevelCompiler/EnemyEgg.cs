using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.LevelCompiler
{
    public class EnemyEgg
    {
        public EnemyEgg(int enemyPrefabIndex, int bornPointIndex)
        {
            this.bornPointIndex = bornPointIndex;
            this.enemyPrefabIndex = enemyPrefabIndex;
        }

        public int bornPointIndex;

        public int enemyPrefabIndex;
    }
}
