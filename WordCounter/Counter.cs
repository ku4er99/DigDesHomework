using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCounter
{
    public class Counter
    {
        private static Dictionary<string, int> GoCount(string Text)
        {
            string[] textArray = Text.Split(new char[] { ' ', '-', ':', '.', '"', '\'', '!', '?', '\n', '[', ']', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            var result = textArray.GroupBy(x => x).Where(x => x.Count() > 1)
                .ToDictionary(key => key.Key, value => value.Count())
                .OrderByDescending(q => q.Value).ToDictionary(key => key.Key, value => value.Value); 
            return result;
        }
    }
}
