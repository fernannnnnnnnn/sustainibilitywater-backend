using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sustainibility_water_monitoring_backend.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;


namespace sustainibility_water_monitoring_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterProfileController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();


        [HttpPost]
        public IActionResult GetDataProfile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getUserCloneByUsrId", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        private string HashPassword(string plainText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        [HttpPost]
        public IActionResult ChangePasswordProfile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());

                // Hash semua password jika ada
                if (value["newPassword"] != null)
                {
                    string plainPassword = value["newPassword"].ToString();
                    string hashedPassword = HashPassword(plainPassword);
                    value["newPassword"] = hashedPassword;
                }

                Console.WriteLine(value.ToString(Newtonsoft.Json.Formatting.Indented));

                dt = lib.CallProcedure("stn_changepasswordprofil", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));

            
            }
            catch { return Ok(JsonConvert.SerializeObject(dt)); }
        }

        [HttpPost]
        public IActionResult UpdateDataProfile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(data.ToString());
                dt = lib.CallProcedure("stn_updateUserClone", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return Ok(JsonConvert.SerializeObject(dt)); }
        }

     

        [HttpPost]
        public IActionResult CreateDataProfile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());

                // Hash semua password jika ada
                if (value["password"] != null)
                {
                    string plainPassword = value["password"].ToString();
                    string hashedPassword = HashPassword(plainPassword);
                    value["password"] = hashedPassword;
                }

                Console.WriteLine(value.ToString(Newtonsoft.Json.Formatting.Indented));

                dt = lib.CallProcedure("stn_createUserClone", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
