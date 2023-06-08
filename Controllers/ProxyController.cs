using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ProxyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public ProxyController(IHttpClientFactory httpClientFactory , ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet("/proxy/image")]
        public async Task<IActionResult> GetImage(string url)
        {
            try
            {
                // Create a new HttpClient and set the user agent
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

                // Send a GET request to the specified URL
                var response = await httpClient.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response stream as a byte array
                    var content = await response.Content.ReadAsByteArrayAsync();

                    // Determine the content type based on the file extension
                    var contentType = "image/png";
                    if (url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "image/jpeg";
                    }
                    else if (url.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "image/gif";
                    }

                    // Return the byte array as an image with the appropriate content type
                    return File(content, contentType);
                }
                else
                {
                    // If the response is not successful, log the error and return a 404 error
                    var message = $"Error retrieving image from URL: {url}. Status code: {response.StatusCode}";
                    _logger.LogError(message);
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, log the error and return a 500 error
                var message = $"Error retrieving image from URL: {url}. Exception: {ex.Message}";
                _logger.LogError(ex, message);
                return StatusCode(500);
            }
        }
    }
}
