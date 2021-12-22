using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection); //core katmanı dahil tüm injectionları yapmamızı sağlar.
        }//apimizin service bağımlılıklarını ya da araya girmesini istediğimiz serviceleri yazarız.
         //this neyi genişletmek istediğimiz anlamına geliyor.
    }
}
