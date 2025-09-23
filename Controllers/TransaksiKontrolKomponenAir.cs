using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sustainibility_water_monitoring_backend.Helper;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;

namespace sustainibility_water_monitoring_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransaksiKontrolKomponenAir(IConfiguration configuration) : Controller
    {
        //readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(PolmanAstraLibrary.PolmanAstraLibrary.Decrypt(configuration.GetConnectionString("DefaultConnection"), "PoliteknikAstra_ConfigurationKey"));
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataTrsKontrolKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_getDataTrsKontrolKomponenAir", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }   
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult UpdateTrsKontrolKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_updateTrsKontrolKomponenAir", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        


        [HttpPost]
        public IActionResult GetDataKomponenAirByBocor([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_getDataKomponenAirByBocor", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        

        [HttpPost]
        public IActionResult CreateTrsKontrolKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_createTrsKontrolKomponenAir", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        
        }

        [HttpPost]
        public IActionResult UpdateStatusSelesaiDeskripsiKontrolKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_updateStatusSelesaiDeskripsiTrsKontrolKomponenAir", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetStatusKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_getStatusKomponenAir", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataTrsKontrolKomponenAirById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_getDataTrsKontrolKomponenAirById", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult UpdateStatusTrsKontrolKomponenAir([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                Console.WriteLine(value.ToString());
                dt = lib.CallProcedure("stn_updateStatusTrsKontrolKomponenAir", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //spnya sama sm yg +Deskripsi

        //        alter PROCEDURE[dbo].[stn_updateStatusSelesaiTrsKontrolKomponenAir]
        //        @p1 VARCHAR(MAX), @p2 VARCHAR(MAX), @p3 VARCHAR(MAX), @p4 VARCHAR(MAX), @p5 VARCHAR(MAX),
        //    @p6 VARCHAR(MAX), @p7 VARCHAR(MAX), @p8 VARCHAR(MAX), @p9 VARCHAR(MAX), @p10 VARCHAR(MAX),
        //    @p11 VARCHAR(MAX), @p12 VARCHAR(MAX), @p13 VARCHAR(MAX), @p14 VARCHAR(MAX), @p15 VARCHAR(MAX),
        //    @p16 VARCHAR(MAX), @p17 VARCHAR(MAX), @p18 VARCHAR(MAX), @p19 VARCHAR(MAX), @p20 VARCHAR(MAX),
        //    @p21 VARCHAR(MAX), @p22 VARCHAR(MAX), @p23 VARCHAR(MAX), @p24 VARCHAR(MAX), @p25 VARCHAR(MAX),
        //    @p26 VARCHAR(MAX), @p27 VARCHAR(MAX), @p28 VARCHAR(MAX), @p29 VARCHAR(MAX), @p30 VARCHAR(MAX),
        //    @p31 VARCHAR(MAX), @p32 VARCHAR(MAX), @p33 VARCHAR(MAX), @p34 VARCHAR(MAX), @p35 VARCHAR(MAX),
        //    @p36 VARCHAR(MAX), @p37 VARCHAR(MAX), @p38 VARCHAR(MAX), @p39 VARCHAR(MAX), @p40 VARCHAR(MAX),
        //    @p41 VARCHAR(MAX), @p42 VARCHAR(MAX), @p43 VARCHAR(MAX), @p44 VARCHAR(MAX), @p45 VARCHAR(MAX),
        //    @p46 VARCHAR(MAX), @p47 VARCHAR(MAX), @p48 VARCHAR(MAX), @p49 VARCHAR(MAX), @p50 VARCHAR(MAX)
        //AS
        //BEGIN
        //    SET NOCOUNT ON;
        //	--ubah status dari rencana perbaikan, sedang di perbaiki, selesai
        //    UPDATE stn_trrencanakontrolkomponenair
        //    set trk_deskripsi_kerusakan = @p2,
        //		trk_tanggal_aktual_selesai = GETDATE(),
        //		trk_modif_by = @p3,
        //		trk_modif_date = GETDATE()

        //    where trk_id = @p1;
        //        END
        //                [HttpPost]
        //        public IActionResult UpdateStatusSelesaiKontrolKomponenAir([FromBody] dynamic data)
        //        {
        //            try
        //            {
        //                JObject value = JObject.Parse(data.ToString());
        //                Console.WriteLine(value.ToString());
        //                dt = lib.CallProcedure("stn_updateStatusSelesaiTrsKontrolKomponenAir", EncodeData.HtmlEncodeObject(value));
        //                return Ok(JsonConvert.SerializeObject(dt));
        //            }
        //            catch { return BadRequest(); }
        //        }




    }
}