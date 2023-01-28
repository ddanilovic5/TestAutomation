using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelpers.Extensions
{
    public static class StringArrayToStringExtension
    {
        public static string ToStringAll(this ICollection collection)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var i = 0;
            var count = collection.Count;
            foreach (var elem in collection)
            {
                sb.Append("\"" + elem + "\"");
                if (i < (count - 1))
                {
                    sb.Append(", ");
                }
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
