using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Aggregator
{
    public class FakeDefinedAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {

            List<string> results = new List<string>();
            var contentBuilder = new StringBuilder();

            contentBuilder.Append("{");

            foreach (var down in responses)
            {
                string content = string.Empty;
                using (var reader = new StreamReader(await down.Items.DownstreamResponse().Content.ReadAsStreamAsync()))
                {
                    content = reader.ReadToEnd();

                    // Do something
                }

                results.Add($"\"{down.Items.DownstreamRequest().ToUri()}\":{content}");
            }
          
            results.Add($"\"master\":\"请求聚合\"");

            contentBuilder.Append(string.Join(",", results));
            contentBuilder.Append("}");

            var stringContent = new StringContent(contentBuilder.ToString())
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            var headers = responses.SelectMany(x => x.Response.Headers);
            return await Task.FromResult(new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<Header>() { }, "some reason"));
        }
    }
}
