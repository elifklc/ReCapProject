using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _iuserService;

        public UsersController(IUserService iuserService)
        {
            _iuserService = iuserService;
        }

        [HttpGet("getall")]

        public IActionResult GetAll()
        {
            var result = _iuserService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]

        public IActionResult Add(User user)
        {
            var result = _iuserService.Add(user);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]

        public IActionResult Delete(User user)
        {
            var result = _iuserService.Delete(user);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]

        public IActionResult Update(User user)
        {
            var result = _iuserService.Update(user);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }
    }
}
