using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sustainibility_water_monitoring_backend.Helper;
using MQTTnet;
using MQTTnet.Client;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Client.Options;
using sustainibility_water_monitoring_backend.Service;


namespace sustainibility_water_monitoring_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Menambahkan MQTTService sebagai singleton
            builder.Services.AddSingleton<MQTTService>();
            /*builder.Services.AddHostedService<MQTTHostedService>();*/

            // Mengaktifkan CORS dan logging
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policyBuilder =>
                    {
                        policyBuilder.WithOrigins( "http://localhost:5173", "http://172.20.10.2:8081", "http://192.168.0.119:8081", "http://10.1.5.2:8081", "http://10.127.212.240:8081")
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                    });
            });

            builder.Services.AddLogging(config => {
                config.AddConsole();
                config.AddDebug();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }

 /*   public class MQTTHostedService : BackgroundService
    {
        private readonly MQTTService _mqttService;

        public MQTTHostedService(MQTTService mqttService)
        {
            _mqttService = mqttService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _mqttService.ConnectAsync();
            await _mqttService.SubscribeAsync("sensor/air/flow");
            Console.WriteLine("MQTT connected and subscribed in background service");
        }
    }*/


    public class MqttService
    {
        private IMqttClient _mqttClient;
        private IMqttClientOptions _options;

        public MqttService()
        {
            // Membuat objek MQTT client
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Menentukan opsi koneksi MQTT
            _options = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.mqtt-dashboard.com", 1883) // Ganti dengan alamat broker MQTT Anda
                .WithCleanSession()
                .Build();

            // Menangani event ketika berhasil terhubung
            _mqttClient.UseConnectedHandler(e =>
            {
                Console.WriteLine("Connected to MQTT broker.");
            });

            // Menangani event ketika terputus
            _mqttClient.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("Disconnected from MQTT broker.");
            });

            // Menangani event ketika menerima pesan
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("Message received: " + Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
            });
        }

        // Metode untuk menghubungkan client ke broker
        public async Task ConnectAsync()
        {
            try
            {
                await _mqttClient.ConnectAsync(_options, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting: " + ex.Message);
            }
        }

        // Metode untuk subscribe ke topik MQTT
        public async Task SubscribeAsync(string topic)
        {
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        // Metode untuk publish pesan ke topik
        public async Task PublishAsync(string topic, string message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithAtMostOnceQoS()
                .Build();

            await _mqttClient.PublishAsync(mqttMessage);
        }

        // Metode untuk disconnect dari broker
        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }
    }
}
