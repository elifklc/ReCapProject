using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    //jwt için.
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //IHttpContextAccessor, HttpContext adı üstünde jwt'ye bir istek yolluyoruz sonuçta oraya 1000lerce istek yapabilir. herkese/her bir isteğe) bir tane trade(HttpContext) oluşur.

        public SecuredOperation(string roles) //bana rolleri ver. roller , ile ayrılarak gelir; attribute olduğu için.
        {
            _roles = roles.Split(','); //Split metni ayırıp array'e atıyor.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //solution'ta 2 paket kuracağız. autofac, autofac.extension.dependencyinjection, autofac.dynamiC.proxy ekle. 
            //ServiceTool bizim injection altyapımızı aynen okuyabilmezi yarayan bir araç olacak.
            //aspect'e injecte edemiyoruz.
            //injection mantığını aspectlerde böyle almış oluyoruz.
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
