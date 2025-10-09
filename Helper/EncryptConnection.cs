using Microsoft.AspNetCore.Mvc;
using PolmanAstraLibrary;

namespace sustainibility_water_monitoring_backend.Helper
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EncryptConnection : Controller
    {
        [HttpGet]
        public IActionResult GetEncryptedString([FromQuery] string connStr)
        {
            try
            {
                string key = "PoliteknikAstraSustainAir_ConfigurationKey";

                // Panggil library standar Astra untuk enkripsi
                string encrypted = PolmanAstraLibrary.PolmanAstraLibrary.Encrypt(connStr, key);

                return Ok(new
                {
                    success = true,
                    original = connStr,
                    encrypted = encrypted
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}
