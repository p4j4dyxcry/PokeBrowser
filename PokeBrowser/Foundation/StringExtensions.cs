using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PokeBrowser.Foundation
{
    public static class StringExtensions
    {
        private const CompareOptions Ordinal = CompareOptions.Ordinal;

        public static int Compare(this string string1, string string2, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.Compare(string1, string2, options);

        public static int Compare(this string string1, int offset1, string string2, int offset2,
            CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.Compare(string1, offset1, string2, offset2, options);

        public static int Compare(this string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.Compare(string1, offset1, length1, string2, offset2, length2, options);

        public static bool Contains(this string source, char value, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, options) >= 0;

        public static bool Contains(this string source, string value, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, options) >= 0;

        public static bool Contains(this string source, char value, int startIndex, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, options) >= 0;

        public static bool Contains(this string source, string value, int startIndex, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, options) >= 0;

        public static bool Contains(this string source, char value, int startIndex, int count, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, count, options) >= 0;

        public static bool Contains(this string source, string value, int startIndex, int count, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, count, options) >= 0;

        public static bool Equals(this string string1, string string2, CompareOptions options = Ordinal) =>
            string1.Compare(string2, options) == 0;

        public static bool Equals(this string string1, int offset1, string string2, int offset2, CompareOptions options = Ordinal) =>
            string1.Compare(offset1, string2, offset2, options) == 0;

        public static bool Equals(this string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options = Ordinal) =>
            string1.Compare(offset1, length1, string2, offset2, length2, options) == 0;

        public static bool IsPrefixOptions(this string source, string prefix, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IsPrefix(source, prefix, options);

        public static bool IsSuffixOptions(this string source, string suffix, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IsSuffix(source, suffix, options);

        private static int IndexOf(this string source, char value, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, options);

        public static int IndexOf(this string source, string value, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, options);

        public static int IndexOf(this string source, char value, int startIndex, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, options);

        public static int IndexOf(this string source, string value, int startIndex, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, options);

        public static int IndexOf(this string source, char value, int startIndex, int count, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, count, options);

        public static int IndexOf(this string source, string value, int startIndex, int count, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, count, options);

        public static int LastIndexOf(this string source, char value, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(source, value, options);

        public static int LastIndexOf(this string source, string value, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(source, value, options);

        public static int LastIndexOf(this string source, char value, int startIndex, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(source, value, startIndex, options);

        public static int LastIndexOf(this string source, string value, int startIndex, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(source, value, startIndex, options);

        public static int LastIndexOf(this string source, char value, int startIndex, int count, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex, count, options);

        public static int LastIndexOf(this string source, string value, int startIndex, int count, CompareOptions options = Ordinal) =>
            CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(source, value, startIndex, count, options);
    }
}
