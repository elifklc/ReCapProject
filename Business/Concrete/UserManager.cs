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
    public class UserManager:IUserService
    {
        IUserDal _iuserDal;

        public UserManager(IUserDal iUserDal)
        {
            _iuserDal = iUserDal;
        }

        public IResult Add(User user)
        {
            //business codes

            if (user.FirstName.Length < 2)
            {
                return new ErrorResult(Messages.PasswordDescriptionInvalid);
            }
            _iuserDal.Add(user);

            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            if(user.Password.Length<1)
            {
                return new ErrorResult(Messages.UserDeleted);
            }
            _iuserDal.Delete(user);

            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_iuserDal.GetAll(), Messages.UserListed);
        }

        public IResult Update(User user)
        {
            _iuserDal.Add(user);
            throw new NotImplementedException();
        }
    }
}



