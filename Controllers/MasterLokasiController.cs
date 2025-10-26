using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sustainibility_water_monitoring_backend.Helper;
using System.Data;

namespace sustainibility_water_monitoring_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterLokasiController(IConfiguration configuration) : Controller
    {
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstra_ConfigurationKey"));
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstraSustainAir_ConfigurationKey"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getDataLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataKomponenByLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("sp_GetKomponenByLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataLokasiById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getDataLokasiById", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreateLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_createLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CheckLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_checkLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DetailLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_detailLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult EditLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_editLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult SetStatusLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_setStatusLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
        [HttpPost]
        public IActionResult GetListLokasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getListLokasi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
        [HttpPost]
        public IActionResult GetListLokasi2([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getListLokasi2", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
    }
}
