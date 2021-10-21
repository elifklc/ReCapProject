using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _ibrandDal;

        public BrandManager(IBrandDal ibrandDal)
        {
            _ibrandDal = ibrandDal;
        }
         
        public IResult Add(Brand brand)
        {
            if (brand.BrandName.Length < 2 )
            {
                return new ErrorResult(Messages.BrandDescriptionInvalid);
            }
           return new SuccessResult(Messages.BrandAdded);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_ibrandDal.GetAll(),Messages.BrandListed); 
        }
    }
}
