using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
        public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor //IInterceptor, autofac'in interceptor özelliği var. 
        {
            public int Priority { get; set; }  //hangş attribute önce çalışsın => Priority

            public virtual void Intercept(IInvocation invocation)
            {

            }
        }
}

