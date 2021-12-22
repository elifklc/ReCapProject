using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection; //.GetService<IMemoryCache>() burada çözdük.
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern desenini uyguladık.
        //Microsoftun kendi kütüphanesini kullanacağız.

        IMemoryCache _memoryCache;// ctordan enjecte etsek çalışmaz çünkü zincir farklı. bağımlılık zincirinin içinde değil. go to core module :)

        public MemoryCacheManager()
        { //coremodule'da arka planda oluşan memory cache instance'ı istiyoruz.
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); //bir desktop appden de çekebilirsin(injecte edilmiş interfaceleri).
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));//cache'e değer ekleyebiliriz.duration, ne kadar süre verirsek o kadar süre. cachede kalacak süre.
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)//bellekte böyle bir cache değeri var mı demek.
        {
            return _memoryCache.TryGetValue(key, out _); //bir şey döndürmek istemediğimde "out _" tekniğini kullanırız. sadece key'i istiyorum oradaki değeri istemiyorum.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern) //bellekten silmeye yarıyor çalışma anında. bunu reflection ile yaparız. kodu çalışma anında oluşturma ya da müdahale etme gibi şeyleri reflection ile yaparız.
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //cacheleme yaptığımda bunu EntriesCollection'nin içine atarım, MemoryCache. yukarıda EntriesCollection bul diyoruz öncelikle.
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            //definition'ı _memoryCache olanları bul. 
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection) //her bir cache elemanını gez.
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase); //pattern'ı bu şekilde oluşturmuş olacağım.
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); //bu kuralı uyanları silme işlemini gerçekleştirirken vereceğim değeri oluştracak.

            foreach (var key in keysToRemove) //uyanların key'ini buluyorum.
            {
                _memoryCache.Remove(key); //ve uçuruyorum.
            }
        }
    }
}

//bunlar MemoryCache'nin dokümantasyonunda bulabilirsin.