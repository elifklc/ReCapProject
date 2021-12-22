using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch; //timer, bu metod ne kadar sürecek.

        public PerformanceAspect(int interval) //PerformanceAspect çağırınca burada bir interval veriyor. interval dediği örneğin geçen 5 sn.
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>(); //Stopwatch, kronometre.
        }


        protected override void OnBefore(IInvocation invocation) //metodun önünde kronometreyi başlatıyorum.
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval) //geçen süreyi hesaplıyorum. geçen süre örneğin 5 snden büyükse,
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}"); 
            }
            _stopwatch.Reset();
        }
    }
}