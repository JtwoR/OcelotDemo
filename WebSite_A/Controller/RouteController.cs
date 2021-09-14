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
    public class RouteController : ControllerBase
    {
        [HttpGet("Get")]
        public async Task<string> Get() {
            return await Task.FromResult("WebSite_A：/api/Route/Get");
        }

        [HttpGet("GetWait")]
        public async Task<string> GetWait()
        {
            System.Threading.Thread.Sleep(10000);
            return await Task.FromResult("WebSite_A：/api/Route/GetWait");
        }

        [HttpGet("GetTime")]
        public async Task<string> GetTime()
        {
            return await Task.FromResult(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
