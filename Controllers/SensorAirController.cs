using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace sustainibility_water_monitoring_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorAirController : ControllerBase
    {
        private readonly string connectionString = "Server=localhost;Database=DB_Sustainability;user id=sa;password=Z1danfernanda.;";

        // Model SensorData didefinisikan di dalam SensorController
        public class SensorDataAir
        {
            public int kpn_id { get; set; }
            public double? volume { get; set; }
            public int? durasi { get; set; }     // durasi aliran air dalam detik (opsional)
            public string? status { get; set; }  // status aliran seperti "bocor", "normal", atau "-"
        }

        [HttpPost("AddDataAir")]
        public async Task<IActionResult> AddDataAir([FromBody] SensorDataAir data)
        {
            if (data == null || data.volume == null)
            {
                return BadRequest("Invalid data.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("stn_createDataSensor", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@kpn_id", data.kpn_id);
                command.Parameters.AddWithValue("@sns_volume", data.volume);
                command.Parameters.AddWithValue("@sns_status", data.status);
                command.Parameters.AddWithValue("@sns_durasi", data.durasi);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return Ok("Data added successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }
        }
    }
}
