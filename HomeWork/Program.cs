using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;


namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string AllText = File.ReadAllText("Warlockshadow.txt");
            string Write = @"Words.txt";
            //-------REFLECTION----------//

            var types = Assembly.LoadFrom("WordCounter.dll").GetTypes();
            var type = types.FirstOrDefault(x => x.Name == "Counter");
            var exemp = Activator.CreateInstance(type);
            var method = exemp.GetType().GetMethod("GoCount", System.Reflection.BindingFlags.NonPublic | BindingFlags.Static);
            Dictionary<string, int> rslt = (Dictionary<string,int>)method.Invoke(null, new object[] {AllText});

            //--------------------------//
           
            try
            {
                using (StreamWriter sw = new StreamWriter(Write, false, System.Text.Encoding.Default))
                {
                    foreach (var word in rslt) {
                        await sw.WriteLineAsync($"Word: {word.Key}. Count: {word.Value}");
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