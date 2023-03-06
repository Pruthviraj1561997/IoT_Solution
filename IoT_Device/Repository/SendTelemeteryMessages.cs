using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using IoT_Device.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;


namespace IoT_Device.Repository
{
    public class SendTelemeteryMessages
    {
        private static string connectionString = "HostName=pruthviiothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=xceTcl8lcOWqo0k+i8Jx6CYjG5weC+dGKU6BTt3bV88=";

        public static RegistryManager registryManager;

        public static DeviceClient client = null;

        public static string myDeviceConnection = "HostName=pruthviiothub.azure-devices.net;DeviceId=test;SharedAccessKey=wa+G3FxM5/605t5dXUNIM0BMjQJLqr3hZUzhN/ZVgWs=";

        public static async Task SendMessage(string deviceName)
        {
            try
            {
                registryManager=RegistryManager.CreateFromConnectionString(connectionString);
                var device = await registryManager.GetTwinAsync(deviceName);
                ReportedProperties reportedProperties = new ReportedProperties();
                TwinCollection twinCollection;
                twinCollection = device.Properties.Reported;
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                while(true)
                {
                    var telemetry = new
                    {
                        temperature = twinCollection["temperature"],
                     ///   drift = twinCollection["drift"],
                      ///  accurarcy = twinCollection["accurarcy"],
                     //   fullscale = twinCollection["fullscale"],
                     //   pressure = twinCollection["pressure"],
                       // supplyVoltageLevel = twinCollection["supplyVoltageLevel"],
                        frequency = twinCollection["frequency"],
                      //  resolution = twinCollection["resolution"],
                        dateTimeLastAppLaunch = twinCollection["dateTimeLastAppLaunch"]
                       // sensorType = twinCollection["sensorType"],


                    };

                    var telemetrys = JsonConvert.SerializeObject(telemetry);
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telemetrys));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0}>Sending message:{1}",DateTime.Now,telemetrys);
                    await Task.Delay(1000);

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
