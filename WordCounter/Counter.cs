using System.Collections.Generic;
using System.Linq;

namespace WordCounter
{
    class Counter
    {
        private static Dictionary<string, int> GoCount(string AllText)
        {
            var AllWords = AllText.Split(' ', '-', ':', '.', '"', '\'', '!', '?').Where(q => !string.IsNullOrEmpty(q));
            var Uniqs = AllWords.Select(q => q.ToLower().Trim()).Distinct();
            var rslt = new Dictionary<string, int>();
            foreach (var word in Uniqs)
            {
                rslt.Add(word, AllWords.Count(q => q.ToLower().Equals(word)));
            }
            rslt = rslt.OrderByDescending(q => q.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
            return rslt;
        }
    }
}
