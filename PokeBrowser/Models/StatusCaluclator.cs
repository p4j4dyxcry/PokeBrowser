namespace PokeBrowser.Models
{
    public class StatusCalculator
    {
        public static int CalcHitPoint(int baseStat , int iv , int ev ,int level)
        {
            return (int)((baseStat * 2 + iv + ev / 4) * level / 100 + level + 10);
        }
        public static int CalcParameter(int baseStat, int iv, int ev, double person, int level)
        {
            return (int)(((baseStat * 2 + iv + ev / 4) * level / 100 + 5) * person);
        }
    }
}
