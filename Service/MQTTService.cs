using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using sustainibility_water_monitoring_backend.Models;  // Impor SensorData dari Models

namespace sustainibility_water_monitoring_backend.Service
{
    public class MQTTService
    {
        private IMqttClient _mqttClient;

        public MQTTService()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            /*            _mqttClient.UseApplicationMessageReceivedHandler(e =>
                        {
                            var topic = e.ApplicationMessage.Topic;
                            var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Console.WriteLine($"Pesan diterima dari topik {topic}: {message}");

                            // Mengirim data ke API dengan format yang tepat
                            var sensorData = new SensorData
                            {
                                kpn_id = GetKpnIdFromTopic(topic),
                                volume = float.Parse(message)
                            };

                            SendDataToApi(sensorData);
                        });*/

            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine($"Pesan diterima dari topik {topic}: {message}");

                try
                {
                    // Jika kpn_id tidak dikirim dari ESP32, bisa override dari topic
                    var sensorData = JsonSerializer.Deserialize<SensorData>(message);

                    if (sensorData != null)
                    {
                        Console.WriteLine("SensorData parsed:");
                        Console.WriteLine($"kpn_id: {sensorData.kpn_id}");
                        Console.WriteLine($"volume: {sensorData.volume}");
                        Console.WriteLine($"durasi: {sensorData.durasi}");
                        Console.WriteLine($"status: {sensorData.status}");

                        SendDataToApi(sensorData);
                    }
                    else
                    {
                        Console.WriteLine("sensorData is null after deserialization.");
                    }

                    Console.WriteLine($"Pesan diterima dari : {sensorData}");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gagal parse data sensor: {ex.Message}");
                }
            });

        }

        private int GetKpnIdFromTopic(string topic)
        {
            if (topic.Contains("hulu"))
                return 21;
            if (topic.Contains("hilir1"))
                return 22;
            if (topic.Contains("hilir2"))
                return 23;
            return 0;
        }

        private async void SendDataToApi(SensorData data)
        {
            using (var client = new HttpClient())
            {
                //var url = "http://172.20.10.5:5255/api/SensorAir/AddDataAir";
                //var url = "http://10.1.5.2:5255/api/SensorAir/AddDataAir";
                var url = "http://10.127.212.240:5255/api/SensorAir/AddDataAir";


                var jsonPayload = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data successfully sent to the API.");
                }
                else
                {
                    Console.WriteLine($"Failed to send data. Status Code: {response.StatusCode}");
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response body: " + error);
                }
            }
        }


        public async Task ConnectAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId("AspNetCoreClient")
                //.WithTcpServer("10.1.5.2", 1884)
                .WithTcpServer("10.127.212.240", 1884)
                .Build();

            await _mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        public async Task PublishMessageAsync(string topic, string message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(message))
                .WithExactlyOnceQoS()
                .WithRetainFlag(false)
                .Build();

            if (!_mqttClient.IsConnected)
            {
                await ConnectAsync();
            }

            await _mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
        }

        public async Task SubscribeAsync(string topic)
        {
            if (!_mqttClient.IsConnected)
            {
                await ConnectAsync();
            }

            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .WithExactlyOnceQoS()
                .Build());

            Console.WriteLine($"Berhasil berlangganan ke topik {topic}");
        }
    }
}
