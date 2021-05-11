using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Entity_Framework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {


        [HttpGet]
        public ContentResult Index()
        {
            var path = @"wwwroot\admin\index.html";
            return new ContentResult
            {

                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = new String(System.IO.File.ReadAllText(path))
        };
        }
    }
}
