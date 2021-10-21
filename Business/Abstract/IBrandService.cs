using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService //istediğim operayonları burada çağıracağım.
    {
        IDataResult<List<Brand>> GetAll();
        IResult Add(Brand brand);
    }
}
