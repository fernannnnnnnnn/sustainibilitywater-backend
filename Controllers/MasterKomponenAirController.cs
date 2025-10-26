using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sustainibility_water_monitoring_backend.Helper;
using System.Data;

namespace sustainibility_water_monitoring_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterKomponenAirController(IConfiguration configuration) : Controller
    {
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstra_ConfigurationKey"));
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstraSustainAir_ConfigurationKey"));
        DataTable dt = new();

        [HttpPost]
        [HttpPost]
        public IActionResult GetDataKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());

                // Asumsi dt adalah DataTable
                DataTable dt = lib.CallProcedure("stn_getDataKomponenAir", EncodeData.HtmlEncodeObject(value));

                // TAMBAHKAN PENGECEKAN INI:
                if (dt == null)
                {
                    // Jika procedure return null, buat DataTable kosong
                    // agar bisa diserialisasi menjadi '[]'
                    dt = new DataTable();
                }
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception ex) // Sebaiknya tangkap Exception-nya
            {
                // Anda bisa tambahkan logging di sini jika perlu
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetDataKomponenAirById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getDataKomponenAirById", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreateKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_createKomponenAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DetailKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_detailKomponenAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DetailLogKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_detailLogKomponen", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreateKomponenAirMobile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_createKomponenAirMobile", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult EditKomponenAirMobile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_editKomponenAirMobile", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult EditKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_editKomponenAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult SetStatusKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_setStatusKomponenAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
        
    }
}
