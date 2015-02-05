using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class TankEgg
    {
        public TankEgg(int enemyPrefabIndex, int bornPointIndex)
        {
            this.bornPointIndex = bornPointIndex;
            this.enemyPrefabIndex = enemyPrefabIndex;
        }

        public int bornPointIndex;

        public int enemyPrefabIndex;
    }
