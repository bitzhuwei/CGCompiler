using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TankEgg
{
    public override string ToString()
        {
            if (this.IsStop())
            {
                return string.Format("|");
            }
            else
            {
                return string.Format("{0},{1}", enemyPrefabIndex, bornPointIndex);
            }
        }
    public bool IsStop()
    {
        return !(enemyPrefabIndex >= 0 && bornPointIndex >= 0);
    }
    public TankEgg(int enemyPrefabIndex, int bornPointIndex)
    {
        this.bornPointIndex = bornPointIndex;
        this.enemyPrefabIndex = enemyPrefabIndex;
    }

    public int bornPointIndex;

    public int enemyPrefabIndex;
}
