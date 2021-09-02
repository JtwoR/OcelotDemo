using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite_A.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet("Get")]
        public async Task<string> Get() {
            return await Task.FromResult("Get WebSite_A");
        }

        [HttpPut("Put")]
        public async Task<string> Put()
        {
            return await Task.FromResult("Put WebSite_A");
        }

        [HttpDelete("Delete")]
        public async Task<string> Delete()
        {
            return await Task.FromResult("Delete WebSite_A");
        }

        [HttpOptions("Options")]
        public async Task<string> Options()
        {
            return await Task.FromResult("Options WebSite_A");
        }

        [HttpGet("Health")]
        public async Task<string> Health()
        {
            return await Task.FromResult("Ok");
        }
    }
}
