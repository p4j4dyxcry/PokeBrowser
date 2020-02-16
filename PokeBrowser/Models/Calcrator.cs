using System;
using System.Collections.Generic;
using System.Text;

namespace PokeBrowser.Foundation
{
    public class Calcrator
    {
        public static int CalcHP(int baseStat , int iv , int ev , double person , int level)
        {
            return (int)((baseStat * 2 + iv + ev / 4) * level / 100 + level + 10);
        }
        public static int CalcParametor(int baseStat, int iv, int ev, double person, int level)
        {
            return (int)(((baseStat * 2 + iv + ev / 4) * level / 100 + 5) * person);
        }
    }
}
