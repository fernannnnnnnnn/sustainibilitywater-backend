using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sustainibility_water_monitoring_backend.Helper;
using System.Data;
using System.Runtime.Versioning;
using System.Text;
using System.Security.Cryptography;

namespace sustainibility_water_monitoring_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UtilitiesController(IConfiguration configuration) : Controller
    {
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstra_ConfigurationKey"));
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstraSustainAir_ConfigurationKey"));
        //readonly LDAPAuthentication adAuth = new(configuration);
        DataTable dt = new();

        [HttpPost]
        [SupportedOSPlatform("windows")]
        public IActionResult Login([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                //bool isAuthenticated = adAuth.IsAuthenticated(EncodeData.HtmlEncodeObject(value)[0], EncodeData.HtmlEncodeObject(value)[1]);
                //if (isAuthenticated)
                //{

                Console.WriteLine(value.ToString(Newtonsoft.Json.Formatting.Indented));
                dt = lib.CallProcedure("sso_getAuthenticationSustainability", EncodeData.HtmlEncodeObject(value));
                //dt = lib.CallProcedure("sso_getAuthenticationAir", EncodeData.HtmlEncodeObject(value));
          
                if (dt.Rows.Count == 0)
                    return Ok(JsonConvert.SerializeObject(new { Status = "LOGIN FAILED" }));
                return Ok(JsonConvert.SerializeObject(dt));
                //}
                //return Ok(JsonConvert.SerializeObject(new { Status = "LOGIN FAILED" }));
            }
            catch { return BadRequest(); }
        }

        // Fungsi hashing SHA256
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
        public IActionResult LoginHP([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());

                // Hash password jika ada key "pw"
                if (value["password"] != null)
                {
                    string plainPassword = value["password"].ToString();
                    string hashed = HashPassword(plainPassword);
                    value["password"] = hashed;
                }
                // Encode ke format array (misalnya array string) sebelum dikirim ke stored procedure
                var encodedParams = EncodeData.HtmlEncodeObject(value);

                // Log data encoded yang akan masuk ke SP
                
                // Panggil stored procedure
                dt = lib.CallProcedure("stn_getAuthenticationAir", encodedParams);

                // Cek hasil
                if (dt.Rows.Count == 0)
                    return Ok(JsonConvert.SerializeObject(new { Status = "LOGIN FAILED" }));

                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        //[Authorize]
        [HttpPost]
        public IActionResult GetDataNotifikasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_getDataNotifikasiReact", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataNotifikasiMobile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_getDataNotifikasiReactMobile", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult SetReadNotifikasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_setReadNotifikasiAllReact", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult SetReadNotifikasiMobile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_setReadNotifikasiAllReactMobile", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult GetDataCountingNotifikasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_getCountNotifikasiReact", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListMenu([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_getListMenu", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListProvinsi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("pro_getListProvinsi", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListKabupaten([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("pro_getListKabupaten", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListKecamatan([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("pro_getListKecamatan", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListKelurahan([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("pro_getListKelurahan", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListKaryawan([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("pro_getListKaryawan", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
    }
}