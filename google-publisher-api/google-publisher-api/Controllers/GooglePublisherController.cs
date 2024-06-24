using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.AndroidPublisher.v3.Data;
using google_publisher_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace google_publisher_api.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class GooglePublisherController : Controller
    {
        private readonly IGooglePublisherService _googlePublisherService;
        public GooglePublisherController(IGooglePublisherService googlePublisherService)
        {
            _googlePublisherService = googlePublisherService;
        }

        // GET: /validate/{packageName}
        [HttpGet("validate/{packageName}")]
        public async Task<IActionResult> Get(string packageName)
        {
            try
            {
                return Ok(await _googlePublisherService.ValidateAppPackageName(packageName));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }
      
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

