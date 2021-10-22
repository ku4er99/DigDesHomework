using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCounter
{
    public class Counter
    {
        private static Dictionary<string, int> GoCount(string AllText, IEnumerable<string> AllWords, IEnumerable<string> Uniqs)
        {
            var rslt = new Dictionary<string, int>();
            foreach (var word in Uniqs)
            {
                rslt.Add(word, AllWords.Count(q => q.ToLower().Equals(word)));
            }
            rslt = rslt.OrderByDescending(q => q.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
            return rslt;
        }

        public static Dictionary<string, int> GoCountParallel(string AllText, IEnumerable<string> AllWords, IEnumerable<string> Uniqs)
        {
            var rslt = new ConcurrentDictionary<string, int>();
            Parallel.ForEach(Uniqs, word =>
            {
                rslt.TryAdd(word, AllWords.Count(q => q.ToLower().Equals(word)));
            });
            var rslt2 = rslt.OrderByDescending(q => q.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
            return rslt2;
        }
    }
}
