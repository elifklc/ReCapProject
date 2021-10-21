using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _irentalDal;

        public RentalManager(IRentalDal irentalDal)
        {
            irentalDal = _irentalDal;
        }

        public IResult Add(Rental rental)
        {
            if (rental.ReturnDate!=null)
            {
                _irentalDal.Updated(rental);
                return new SuccessResult(Messages.RentalAdded);
            }

            _irentalDal.Updated(rental);
            return new ErrorResult(Messages.RentalDescriptionInvalid);
        }

        public IResult Delete(Rental rental)
        {
            _irentalDal.Delete(rental);
           return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_irentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int rentId)
        {
            return new SuccessDataResult<Rental>(_irentalDal.Get(r => r.CarId == rentId));
        }

        public IResult Update(Rental rental)
        {
            _irentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }
    }
}
