using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) //.net'in service lerini kullanarak ve onları kendin build et. web apide oluşturduğumuz injectionları oluşturmamıza yarıyor. herhangi bir interface'in service karşılığını bu tool vasıtası ile alabiliriz.
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
