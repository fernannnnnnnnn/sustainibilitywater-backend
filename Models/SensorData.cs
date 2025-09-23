using Microsoft.AspNetCore.Mvc;

namespace sustainibility_water_monitoring_backend.Models
{
    public class SensorData
    {   
        public int kpn_id { get; set; }
        public double? volume { get; set; }
        public int? durasi { get; set; }     // durasi aliran air dalam detik (opsional)
        public string? status { get; set; }  // status aliran seperti "bocor", "normal", atau "-"
    }

}
