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

        // GET /mainstore/listing/{packageName}
        [HttpGet("mainstore/listing/{packageName}")]
        public async Task<IActionResult> GetMainStoreListings(string packageName)
        {
            try
            {
                return Ok(await _googlePublisherService.GetMainStoreListings(packageName));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }

        }

        // GET /mainstore/listing/{packageName}/{language}
        [HttpGet("mainstore/listing/{packageName}/{language}")]
        public async Task<IActionResult> GetMainStoreListingByLanguage(string packageName,string language)
        {
            try
            {
                return Ok(await _googlePublisherService.GetMainStoreListingByLanguage(packageName,language));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }

        }

        // GET /app/{packageName}/details
        [HttpGet("app/{packageName}/details")]
        public async Task<IActionResult> GetAppDetails(string packageName)
        {
            try
            {
                return Ok(await _googlePublisherService.GetAppDetails(packageName));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }

        }

        // GET /app/{packageName}/translations
        [HttpGet("app/{packageName}/translations")]
        public async Task<IActionResult> GetAppCurrentTranslations(string packageName)
        {
            try
            {
                return Ok(await _googlePublisherService.GetAppCurrentTranslations(packageName));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }

        }

        // PUT /app/{packageName}/translations/default/{language}
        [HttpPut("app/{packageName}/translations/default/{language}")]
        public async Task<IActionResult> SetDefaultLanguage(string packageName,string language, [FromQuery] bool changesNotSentForReview = false)
        {
            try
            {
                return Ok(await _googlePublisherService.SetDefaultLanguage(packageName,language,changesNotSentForReview));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }

        }

        // DELETE  /app/{packageName}/translations/{language}
        [HttpDelete("app/{packageName}/translations/{language}")]
        public async Task<IActionResult> RemoveTranslation(string packageName, string language, [FromQuery] bool changesNotSentForReview = false)
        {
            try
            {
                return Ok(await _googlePublisherService.RemoveTranslation(packageName, language,changesNotSentForReview));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }
        }

        // PUT /app/{packageName}/details
        [HttpPut("/app/{packageName}/details")]
        public async Task<IActionResult> UpdateAppDetails(string packageName, [FromBody] Models.GooglePublisherModel.UpdateAppDetailRequest model, [FromQuery] bool changesNotSentForReview = false)
        {
            try
            {
                return Ok(await _googlePublisherService.UpdateAppDetails(packageName, model,changesNotSentForReview));
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., package name not found)
                Console.WriteLine($"Error checking package existence: {ex.Message}");
                return Ok(ex.Message);
            }

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

