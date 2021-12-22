using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) //ctor, attr. 60 dk cachede duracak sonra cacheden imha edilecek. buraya bir inj. yapılmaz.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //hangi cachemanager kullandığımı belirtiyorum. buraya dokunma eğer cache manager kullanmayacaksan burada değil core'da coremoduleda o değişikliği yapacaksın.
        }

        public override void Intercept(IInvocation invocation)  //override Inrecept'e. bu MethodInterception buradan geliyor. getall'u çalıştırmadan bu kodları çalıştırıyorum.
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //{invocation.Method.ReflectedType.FullName} -> Northwind.Business.IProductService
            //alttaki ikisi reflection.                                                                             //.{invocation.Method.Name} -> .GetAll
            var arguments = invocation.Arguments.ToList(); //arguments demek parametreler. invocation.Arguments.ToList(), methodun parametrelerini listeye çevir. 
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //methodun parametrelerini tek tek değeri varsa GetAll methodunun içerisine ekliyorum. parametreyi vermediysek de null geçerim.
            //(linq uygulaması)string.Join, bir araya getir demek. parametrelerin her biri için "," ile bir araya getir. ankara, 5 diye key oluşturuyor.
            if (_cacheManager.IsAdd(key)) //yukarıda key oluşturdum. ifte ise bellekte var mı onu kontrol ediyorum.(böyle bir cache anahtarı) her cachete aynı anahtar oluşur!
            {
                invocation.ReturnValue = _cacheManager.Get(key); //varsa metodu hiç çalıştırmadan geri dön demek.cachemanagerdan get et.
                //metodun return'ü       cachedeki data olsun demek.
                return;
            }
            invocation.Proceed(); //yoksa metodu devam ettir. dbden datayı getirdi.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //anahtar, return value ve duration buraya yani cache eklenir.
        }
    }
}