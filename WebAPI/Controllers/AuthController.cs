using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //api/[auth] diye gelir.
    [ApiController]
    public class AuthController : Controller //servisimize login olabilmek için gerekli operasyonları içeriyor.
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")] //HttpPost, sisteme kayıt atmaya çalışacağız.
        public ActionResult Login(UserForLoginDto userrForLoginDto)
        {
            var userToLogin = _authService.Login(userrForLoginDto); //authmanagerdaki logine gidiyoruz.
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message); //başarısız olma durumunda ne olacak authmanager'ı geliştirmemiz gerekiyor.
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userrForRegisterDto)
        {
            var userExists = _authService.
                UserExists(userrForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message); //api tarafaında nasıl bir sonuç döneceğim benim olayım yani business yazmıyorum burada.
            }

            var registerResult = _authService.Register(userrForRegisterDto, userrForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
