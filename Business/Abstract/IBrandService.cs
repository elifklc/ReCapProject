using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService //istediğim operayonları burada çağıracağım.deneme git.
    {
        IDataResult<List<Brand>> GetAll();
        IResult Add(Brand brand);
    }
}
