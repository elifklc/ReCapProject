using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolver
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//artık memorycachemanagerdaki injection'ın karşılığı olmuş oldu. _memoryCahe'in karşılığı. redise geçersek buna gerek yok ama. sadece alttaki update'i yapamn yeterli. arka planda memory cache instance oluşmuş oluyor.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            //redis yazmak istersen şunu yapman yeterli, serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
//uygulama seviyesinde bağımlılıklarımızı çözecek.