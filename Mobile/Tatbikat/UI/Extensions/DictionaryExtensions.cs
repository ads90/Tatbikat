using System;
using System.Collections.Generic;
using System.Text;

namespace Tatbikat.UI.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToQueryParmaters(this IDictionary<string, object> parameters)
        {
            string q = string.Empty;
            if (parameters.Count > 0)
            {
                StringBuilder query = new StringBuilder("?");
                foreach (KeyValuePair<string, object> p in parameters)
                {
                    query.Append($"{p.Key}={p.Value}&");
                }

                query = query.Remove(query.Length - 1, 1);
                q = Uri.EscapeUriString(query.ToString());
            }

            return q;
        }
    }
}
