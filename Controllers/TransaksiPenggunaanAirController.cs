using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sustainibility_water_monitoring_backend.Helper;
using System.Data;

namespace sustainibility_water_monitoring_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransaksiPenggunaanAirController(IConfiguration configuration) : Controller
    {
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstra_ConfigurationKey"));
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstraSustainAir_ConfigurationKey"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataPenggunaanAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getDataPenggunaanAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
        [HttpPost]
        public IActionResult UpdateDataPenggunaanAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_updatePenggunaanAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
        [HttpPost]
        public IActionResult GetDataWaterConsumption([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getDailyWaterVolumeByMonth", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


        [HttpPost]
        public IActionResult DetailPenggunaanAirHarian([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_detailPenggunaanAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DetailPenggunaanAirHarianMobile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_detailPenggunaanAirMobile", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DetailPenggunaanAirHarianHistoryMobile([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_getDetailPenggunaanAirMobile", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DetailTransaksiKomponen([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_detailKomponenPenggunaanAir", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }



        [HttpPost]
        public IActionResult UpdateDataHulu([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("stn_updatePenggunaanAirHulu", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(JsonConvert.SerializeObject(value));  // Untuk melihat data yang diterima
                Console.WriteLine(JsonConvert.SerializeObject(dt));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
    }
}