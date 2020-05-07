using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace RMSContext.GRPC.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        string grpcServer;
        public ApplicationController(IConfiguration configuration)
        {
            grpcServer = configuration.GetSection("ApplicationSettings").GetValue<string>("GRPCS_SERVER_HOST");
        }

        // GET: api/Application
        [HttpGet]
        public ListResponse Get()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress(grpcServer);
            var client = new ApplicationRPC.ApplicationRPCClient(channel);

            var test = client.List(new ListRequest());

            return test;
        }

        // GET: api/Application/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Application
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Application/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
