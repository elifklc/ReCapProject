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
    public class RentalsController : ControllerBase
    {
        IRentalService _irentalService;

        public RentalsController(IRentalService irentalService)
        {
            _irentalService = irentalService;
        }

        [HttpGet("getall")]

        public IActionResult GetAll()
        {
            var result = _irentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]

        public IActionResult GetById()
        {
            var result = _irentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]

        public IActionResult Add(Rental rental)
        {
            var result = _irentalService.Add(rental);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]

        public IActionResult Delete(Rental rental)
        {
            var result = _irentalService.Delete(rental);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]

        public IActionResult Update(Rental rental)
        {
            var result = _irentalService.Update(rental);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }
    }
}
