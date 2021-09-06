using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            return await Task.FromResult(JsonConvert.SerializeObject(new { msg = "Get WebSite_B" }));
        }

        [HttpPost("Post")]
        public async Task<string> Post()
        {
            return await Task.FromResult("Post WebSite_B");
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

        [HttpGet("Token")]
        public async Task<string> Token()
        {

            string url = $"{GlobalConfig.IdentityserverUrl}/connect/token";

            string jsonContent = "client_id=client&client_secret=secret&grant_type=client_credentials&scope=WebSite";
            string result = string.Empty;

            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                //content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                result = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }

            return await Task.FromResult(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(result), Formatting.Indented));
        }
    }
}
