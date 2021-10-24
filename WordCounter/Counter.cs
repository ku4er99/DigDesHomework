using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCounter
{
    public class Counter
    {
        private static Dictionary<string, int> GoCount(string[] textArray)
        {
            var result = textArray.GroupBy(x => x).Where(x => x.Count() > 1)
                .ToDictionary(key => key.Key, value => value.Count())
                .OrderByDescending(q => q.Value).ToDictionary(key => key.Key, value => value.Value); 
            return result;
        }


        public static Dictionary<string, int> GoCountParallel(string[] textArray)
        {
            var rslt = new ConcurrentDictionary<string, int>();
            var rslt2 = new Dictionary<string, int>();
            Parallel.ForEach(textArray, word =>
            {
                rslt.AddOrUpdate(word, 1, (Word, oldValue) => oldValue+1);
            });
            rslt2 = rslt.OrderByDescending(q => q.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
            return rslt2;
        }
    }
}
