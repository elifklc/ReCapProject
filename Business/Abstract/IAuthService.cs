using Core.Entities.Concrete;
using Core.Utilities;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService //sisteme giriş ve kayıt olmak için. register.
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email); //kullanıcı var mı?
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
