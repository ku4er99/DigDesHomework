using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static string AllText = File.ReadAllText("BIG.txt").ToLower();
        static string Write = @"Words.txt";
        static string[] textArray = AllText.Split(new char[] { ' ', '-', ':', '.', '"', '\'', '!', '?', '\n', '[', ']', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        public static Dictionary<string, int> ReflectionMethod()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //-------REFLECTION----------//
            var types = Assembly.LoadFrom("WordCounter.dll").GetTypes();
            var type = types.FirstOrDefault(x => x.Name == "Counter");
            var exemp = Activator.CreateInstance(type);
            var method = exemp.GetType().GetMethod("GoCount", System.Reflection.BindingFlags.NonPublic | BindingFlags.Static);
            Dictionary<string, int> rslt = (Dictionary<string, int>)method.Invoke(null, new object[] { textArray });
            //--------------------------//

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime with Reflection: " + time);
            return rslt;
        }

        public static Dictionary<string, int> ParallelMethod()
        {
            Dictionary<string, int> rslt = new Dictionary<string, int>();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            rslt = WordCounter.Counter.GoCountParallel(textArray);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime with ConcurrentDictionary & Parallel.ForEach: " + time);
            return rslt;
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Counting the program execution time in the format:");
            Console.WriteLine("[Hours]:[Minutes]:[Seconds].[Milliseconds]");
            Console.WriteLine("-------------------------------------------------------------------------------");
            ReflectionMethod();
            var rslt = ParallelMethod();

            try
            {
                using (StreamWriter sw = new StreamWriter(Write, false, System.Text.Encoding.Default))
                {
                    foreach (var word in rslt) {
                        await sw.WriteLineAsync($"Word: {word.Key}. Count: {word.Value}");
                    }
                }
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("\t\tRecording completed!");
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}