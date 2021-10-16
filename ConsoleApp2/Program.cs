using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string AllText = File.ReadAllText("D:\\Warlockshadow.txt");
            string Write = @"D:\\Words.txt";
            var AllWords = AllText.Split(' ', '-', ':', '.', '"', '\'', '!', '?').Where(q => !string.IsNullOrEmpty(q));
            var Uniqs = AllWords.Select(q => q.ToLower().Trim()).Distinct();
            var rslt = new Dictionary<string, int>();
            foreach (var word in Uniqs)
            {
                rslt.Add(word, AllWords.Count(q => q.ToLower().Equals(word)));
            }
            rslt = rslt.OrderByDescending(q => q.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
            try
            {
                using (StreamWriter sw = new StreamWriter(Write, false, System.Text.Encoding.Default))
                {
                    foreach (var word in rslt) {
                        sw.WriteLineAsync($"Word: {word.Key}. Count: {word.Value}");
                    }
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
