using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public Dictionary<string, int> GoCount(string Text)
        {
            var rslt = new ConcurrentDictionary<string, int>();
            var rslt2 = new Dictionary<string, int>();
            string[] textArray = Text.Split(new char[] { ' ', '-', ':', '.', '"', '\'', '!', '?', '\n', '[', ']', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Parallel.ForEach(textArray, word =>
            {
                rslt.AddOrUpdate(word, 1, (Word, oldValue) => oldValue + 1);
            });
            rslt2 = rslt.OrderByDescending(q => q.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
            return rslt2;
        }
    }
}
