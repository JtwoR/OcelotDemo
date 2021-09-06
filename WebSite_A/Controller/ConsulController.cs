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
    public class ConsulController : ControllerBase
    {
        [HttpGet("Get")]
        public async Task<string> Get()
        {
            return await Task.FromResult("WebSite_A：/api/Consul/Get");
        }

        [HttpGet("GetWait")]
        public async Task<string> GetWait()
        {

            return await Task.FromResult("WebSite_A：/api/Consul/GetWait");
        }

        [HttpGet("Error")]
        public async Task<string> Error()
        {
            throw new Exception("手动异常");
            return await Task.FromResult("WebSite_A：/api/Consul/GetWait");
        }
    }
}
