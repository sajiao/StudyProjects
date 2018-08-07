
using System.Collections.Generic;
namespace DotNet.Common
{
    public static class DictionaryExtensions
    {
        public static T TryGetKey<T,V>(this Dictionary<T, V> source, V values)
        {
            T result = default(T);
            if (source != null && source.Count > 0)
            {
                foreach (KeyValuePair<T, V> kvp in source)
                {
                    if (kvp.Value != null && kvp.Value.ToString().EqualsCurrentCultureIgnoreCase(values.ToString()))
                    {
                        result = kvp.Key;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
