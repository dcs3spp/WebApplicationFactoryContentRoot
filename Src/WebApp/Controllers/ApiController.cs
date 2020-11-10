using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace WebApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly string PingResponse = "pong";
        private readonly ILogger<ApiController> _logger;
        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ping")]
        public string Ping()
        {
            System.Console.WriteLine("Pinged...");
            return PingResponse;
        }
    }
}
