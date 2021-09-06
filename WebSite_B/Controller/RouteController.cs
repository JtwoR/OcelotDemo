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
    public class RouteController : ControllerBase
    {
        [HttpGet("Get")]
        public async Task<string> Get()
        {
            return await Task.FromResult("WebSite_B：/api/Route/Get");
        }

        [HttpGet("GetWait")]
        public async Task<string> GetWait()
        {
            return await Task.FromResult("WebSite_B：/api/Route/GetWait");
        }
    }
}
