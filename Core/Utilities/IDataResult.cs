using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public interface IDataResult<T>: IResult  //interface implementasyonu yaptık.
    {
        T Data { get; }
    }
}
