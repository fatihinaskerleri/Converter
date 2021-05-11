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
            var result = _operationService.GetAll();
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

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UploadFile( IFormFile file)
        {
            Operation operation = new Operation();
            if (CheckIfImageFile(file))
            {
                var extension =  file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string fileName = DateTime.Now.Ticks.ToString(); //Create a new Name for the file due to security reasons.
                await WriteFile(file, fileName + "." + extension);
               
                operation.Foto = @"wwwroot\\Upload\\" + fileName + "." + extension;
                operation.DonusturulenFormat = Request.Form["donusturulenformat"];
                
                operation.YuklenenFormat = extension;


                return Add(operation, fileName);
            }
            else
            {
                return BadRequest("Geçersiz dosya uzantısı");
            }

        }

        private bool CheckIfImageFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" || extension.ToLower() == ".gif" || extension.ToLower() == ".tiff"); 
        }
        private async Task<bool> WriteFile(IFormFile file, string fileName)
        {
            bool isSaveSuccess = false;
            try
            {
                
                

                var pathBuilt = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot\\Upload");

                if (!System.IO.Directory.Exists(pathBuilt))
                {
                    System.IO.Directory.CreateDirectory(pathBuilt);
                }

                var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot\\Upload",
                   fileName);

                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }
        public IActionResult Add(Operation operation, string fileName)
        {
            var result = _operationService.Add(operation, fileName);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
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
