using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception //ne zaman çalışır? data bozulursa; yeni data ekleme, güncelleme, silme olursa; yani veriyi manipüle eden metodlara cacheremoveaspect kullanılır.
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) //data manipulation gerçeklemezse ben niye verimi sileyim? tabiki silmiyorum.
        {
            _cacheManager.RemoveByPattern(_pattern); //pattern'a göre silme işlemi yapar.
        }
    }
}