using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared.Helper
{
    public static class ClassValue
    {
        public static IEnumerable<string> ToValueList<T>(this T obj) where T : class
        {
            List<string> result = new List<string>();
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.GetValue(obj) == null || string.IsNullOrEmpty(p.GetValue(obj).ToString()))
                    continue;
                if (p.CustomAttributes.Where(a => a.AttributeType == typeof(JsonIgnoreAttribute)).Any())
                    continue;

                result.Add(p.GetValue(obj).ToString());
            }
            return result;
        }
    }
}
