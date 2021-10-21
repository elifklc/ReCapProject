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
    public class CarManager:ICarService
    {
        ICarDal _icarDal;  //soyut nesne ile bağlantı kurduk.(injection) Bir iş sınıfı başka sınıfları newleyemez! Bu sebeple injection yaptık.

        public CarManager(ICarDal iCarDal)
        {
            _icarDal = iCarDal;
        }

        public IResult Add(Car car)
        {
            //business codes

            if (car.Description.Length<2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);
            }
            _icarDal.Add(car);

            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            if (car.ModelYear >=2000)
            {
                return new ErrorResult(Messages.CarModelYearInvalid);
            }
            _icarDal.Delete(car);

            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==15)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_icarDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 10)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);  //MaintenanceTime: Bakım zamanı
            }
            return new SuccessDataResult<List<CarDetailDto>>(_icarDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_icarDal.GetAll(c=>c.BrandId==id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_icarDal.GetAll(c=>c.ColorId==id));
        }

        public IResult Update(Car car)
        {
            return new SuccessResult(Messages.CarDeleted);
        }
    }
}
