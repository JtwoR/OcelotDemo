using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite_B.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ValuesController : ControllerBase
    {
        [HttpGet("Get")]
       // [Authorize]
        public async Task<string> Get()
        {
            return await Task.FromResult("Get WebSite_B");
        }

        [HttpPut("Put")]
        public async Task<string> Put()
        {
            return await Task.FromResult("Put WebSite_B");
        }

        [HttpDelete("Delete")]
        public async Task<string> Delete()
        {
            return await Task.FromResult("Delete WebSite_B");
        }

        [HttpOptions("Options")]
        public async Task<string> Options()
        {
            return await Task.FromResult("Options WebSite_B");
        }

        [HttpGet("Health")]
        public async Task<string> Health()
        {
            return await Task.FromResult("Ok");
        }
    }
}
