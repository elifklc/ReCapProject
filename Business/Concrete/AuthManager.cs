using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

    namespace Business.Concrete
    {
        public class AuthManager : IAuthService //kayıt olmak için gerekli operasyonlar.
        {
            private IUserService _userService; //userı kontrol etmek lazım.
            private ITokenHelper _tokenHelper;

            public AuthManager(IUserService userService, ITokenHelper tokenHelper)
            {
                _userService = userService;
                _tokenHelper = tokenHelper;
            }

            public IDataResult<User> Register(UserForRegisterDto userrForRegisterDto, string password)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var userr = new User
                {
                    Email = userrForRegisterDto.Email,
                    FirstName = userrForRegisterDto.FirstName,
                    LastName = userrForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true //Status, aktif mi bilgisi. false verirsek emailde onay isterim ya da sistem yöneticisinden onay istemek gibi örnekler söz konusu olabilir.
                };
                _userService.Add(userr);
                return new SuccessDataResult<User>(userr, Messages.UserrRegistered);
            }

            public IDataResult<User> Login(UserForLoginDto userrForLoginDto)
            {
                var userrToCheck = _userService.GetByMail(userrForLoginDto.Email); //kullanıcı var mo onu kontrol ediyorum.
                if (userrToCheck == null)
                {
                    return new ErrorDataResult<User>(Messages.UserrNotFound);
                }
                //kullanıcıyı bulduk. kullanıcın yolladığı açık bir password var. salt ile hashleyeceğiz. hashleri de en son kıyaslayacağız.
                if (!HashingHelper.VerifyPasswordHash(userrForLoginDto.Password, userrToCheck.PasswordHash, userrToCheck.PasswordSalt)) //Hashinghelper'ı Hashing'in altında ayrıca yazdık.
                {
                    return new ErrorDataResult<User>(Messages.PasswordError);
                }

                return new SuccessDataResult<User>(userrToCheck, Messages.SuccessfulLogin);
            }

            public IResult UserExists(string email)
            {
                if (_userService.GetByMail(email) != null)
                {
                    return new ErrorResult(Messages.UserrAlreadyExists);
                }
                return new SuccessResult();
            }

            public IDataResult<AccessToken> CreateAccessToken(User userr) //frontendde kayıt olan kişiye token vereceğiz.
            {
                var claims = _userService.GetClaims(userr);
                var accessToken = _tokenHelper.CreateToken(userr, claims);
                return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
            }
        }
    }
