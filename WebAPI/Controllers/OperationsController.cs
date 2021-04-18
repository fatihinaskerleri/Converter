using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Entity_Framework;
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
    public class OperationsController : ControllerBase
    {
        IOperationService _operationService;
        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        { 
           var result= _operationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
           return BadRequest(result);
        }

        [HttpGet("getallbyresponse")]
        public IActionResult GetAllByResponse(string response)
        {
            var result = _operationService.GetAllByResponse(response);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _operationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Operation operation)
        {
            var result = _operationService.Add(operation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Operation operation)
        {
            var result = _operationService.Delete(operation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Operation operation)
        {
            var result = _operationService.Update(operation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
