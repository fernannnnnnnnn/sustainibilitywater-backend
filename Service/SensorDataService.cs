﻿using sustainibility_water_monitoring_backend.Controllers;

namespace sustainibility_water_monitoring_backend.Service
{
    public class SensorDataService
    {
        private readonly List<SensorData> _sensorDataList = new List<SensorData>();

        public void AddSensorData(SensorData data)
        {
            _sensorDataList.Add(data);
        }

        public IEnumerable<SensorData> GetSensorData()
        {
            return _sensorDataList;
        }
    }

}
