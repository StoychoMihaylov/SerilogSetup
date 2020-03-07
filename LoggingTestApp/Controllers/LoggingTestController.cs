namespace LoggingTestApp.Controllers
{
    using System;
    using Serilog;
    using LoggingTestApp.Common;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("logging")]
    public class LoggingTestController : ControllerBase
    {
        [HttpGet]
        [Route("1")]
        public IActionResult Get()
        {
            Log.Error(new Exception("Buuuuuuuum") + LogContextInfoConfiger.logCtxInfo(Request));

            return StatusCode(200);
        }

        [HttpPost]
        [Route("2")]
        public IActionResult Post()
        {
            Log.Error(new Exception("Buuuuum!"), "Something wrong boom exc!" + LogContextInfoConfiger.logCtxInfo(Request));

            return StatusCode(200);
        }

        [HttpPut]
        [Route("3")]
        public IActionResult Put()
        {
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("4")]
        public IActionResult Delete()
        {
            return StatusCode(200);
        }
    }
}
