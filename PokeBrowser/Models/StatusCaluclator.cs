using System;

namespace PokeBrowser.Models
{
    public class StatusCalculator
    {
        public static int CalcHitPoint(int baseStat , int iv , int ev ,int level)
        {
            return (int)((baseStat * 2 + iv + ev / 4) * (level / 100d) + level + 10);
        }
        public static int CalcParameter(int baseStat, int iv, int ev, double person, int level)
        {
            return (int)(((baseStat * 2 + iv + ev / 4) * level / 100 + 5) * person);
        }

        public static int CalcHitPointEv(int baseStat , int iv , int param ,int level)
        {
            var n = param - level - 10;
            
            n = (int)Math.Ceiling(n * 100 / (double)level);
            n = (n - baseStat * 2 - iv) * 4;
            if (n < 0)
                n = 0;
            return n;
        }
        
        public static int CalcEv(int baseStat, int iv, int param, double person, int level)
        {
            var n = (int)Math.Ceiling(param / person) - 5;
            
            n = (int)Math.Ceiling(n * 100 / (double)level);
            n = (n - baseStat * 2 - iv) * 4;
            if (n < 0)
                n = 0;
            return n;
        }
    }
}
