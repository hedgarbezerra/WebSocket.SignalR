﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        // GET: api/<SessionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SessionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SessionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SessionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SessionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
