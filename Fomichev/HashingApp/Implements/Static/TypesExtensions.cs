using System;
using System.Linq;

namespace Implements.Static
{
    public static class TypesExtensions
    {
        public static string AsString(this string[] strs, string separator)
            => string.Join(separator, strs);

        public static string[] GetEnumProperties(Type @enum)
            => @enum.GetProperties().Select(x => x.Name).Where(x => x != "None").ToArray();
    }
}
