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
    public class ColorManager : IColorService
    {
        IColorDal _icolorDal;

        public ColorManager(IColorDal iColorDal)
        {
            _icolorDal = iColorDal;
        }

        public IResult Add(Color color)
        {
            if (color.ColorName.Length<4)
            {
                return new ErrorResult(Messages.ColorDescriptionInvalid);
            }
            return new SuccessResult(Messages.ColorAdded);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_icolorDal.GetAll(), Messages.ColorListed);
        }
    }
}
