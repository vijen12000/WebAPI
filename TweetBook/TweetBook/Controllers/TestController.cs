using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TweetBook.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]        
        public IActionResult Index()
        {
            //Update setting in appsettings.json

            return Ok(new { name ="Vijender"});
        }

        [HttpGet,MapToApiVersion("2.0")]        
        public IActionResult IndexV2()
        {
            //Update setting in appsettings.json

            return Ok(new { name = "Vijender V2" });
        }
    }    
}
