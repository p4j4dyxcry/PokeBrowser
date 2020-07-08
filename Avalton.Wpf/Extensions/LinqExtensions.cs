using System.Collections.Generic;
using System.Linq;

namespace Avalton.Wpf.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> @this)
        {
            return @this.Where(x => x != null);
        }

        public static IEnumerable<string> NotNullOrEmpty(this IEnumerable<string> @this)
        {
            return @this.Where(x => x != null);
        }
    }
}