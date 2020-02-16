using System.Globalization;
using System.Linq;

namespace PokeBrowser.Foundation
{
    public static class StringAnalizer
    {
        public static string Extraction(this string @this ,string start , string end)
        {
            var index0 = @this.IndexOf(start) + start.Length;
            var index1 = @this.IndexOf(end) - index0;

            return @this.Substring(index0, index1);
        }

        public static bool ContainByJajp(string[] @targets, string[] values)
        {
            CompareOptions options = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

            return values.All(x =>
            {
                return targets.Any(y => y.Contains(x, options));
            });

        }
    }
}
