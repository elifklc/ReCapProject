using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;  //soyut nesne ile bağlantı kurduk.(injection) Bir iş sınıfı başka sınıfları newleyemez! Bu sebeple injection yaptık.
        IColorService _colorService;

        public CarManager(ICarDal iCarDal, IColorService icolorService)
        {
            _carDal = iCarDal;
            _colorService = icolorService;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]

        public IResult Add(Car car)
        {
            //business codes

            IResult result = BusinessRules.Run(CheckIfCarNameExists(car.Description), CheckIfCarCountOfColorCorrect(car.ColorId), CheckIfColorLimitExceeded());

            _carDal.Add(car);

            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            if (car.ModelYear >= 2000)
            {
                return new ErrorResult(Messages.CarModelYearInvalid);
            }
            _carDal.Delete(car);

            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 10)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);  //MaintenanceTime: Bakım zamanı
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [CacheAspect]

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }

        public IResult Update(Car car)
        {
            return new SuccessResult(Messages.CarDeleted);
        }

        [TransactionScopeAspect] //commentteki sarmal işini görecek. TransactionScopeAspect'te bunu hallettik. db üstünde oluyor bu işlem. nested transaction.
        public IResult AddTransactionTest(Car car)
        {
            //using(TransactionScope = new TransactionScope())
            //{
            //    try
            //    {
            //        if (product.UnitPrice < 10) 
            //        {
            //            throw new Exception("");
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        scope.Dispose();
            //    }
            //} //kötü code

            Add(car); //arabayı ekledi.

            if (car.DailyPrice < 10) //başka bir işlem yaparken hata alırsa
            {
                throw new Exception("");
            }

            Add(car); //114. satıra dön diyoruz.

            return null;
        }

        //iş kuralı parçası
        private IResult CheckIfCarCountOfColorCorrect(int colorId)
        {
            //bir kategoride en fazla 10 ürün olabilir.
            var result = _carDal.GetAll(c => c.ColorId == colorId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfColorError);
            }
            return new SuccessResult();
        }

        //business code
        private IResult CheckIfCarNameExists(string carName)
        {
            //aynı isimde ürün eklenemez.
            var result = _carDal.GetAll(c => c.Description == carName).Any(); //Any(), var mı demek.bool döndürür, data var mı yok mu.
            if (result)
            {
                return new ErrorResult(Messages.CarNameAlreadyExists);
            }
            return new SuccessResult();
        }

        //business code

        private IResult CheckIfColorLimitExceeded()
        {
            //eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.(microservice mimarilere nasıl bakmalıyız?)
            var result = _colorService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.ColorLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
