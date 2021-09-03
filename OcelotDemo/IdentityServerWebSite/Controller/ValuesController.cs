
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IIdentityServerBuilder _identityServerBuilder;
        public ValuesController(IIdentityServerBuilder identityServerBuilder) {
            _identityServerBuilder = identityServerBuilder;
        }


        [HttpGet("ReSet")]
        public ActionResult ReSet() 
        {
    
            return Ok();
        }
    }
}
