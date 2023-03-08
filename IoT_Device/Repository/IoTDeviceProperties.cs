using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IoT_Device.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Options;

namespace IoT_Device.Repository
{
    public class IoTDeviceProperties
    {
        private static string connectionString = "HostName=Demohubpruthvi.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=pBWUBdJrak/gp+oJEp435gpJ7CWOErZ+DEMSTXRmsGg=";
        
        public static RegistryManager registryManager=RegistryManager.CreateFromConnectionString(connectionString);

        public static DeviceClient client = null;

        public static string myDeviceConnection = "HostName=Demohubpruthvi.azure-devices.net;DeviceId=test;SharedAccessKey=uJCUONa1pFLF4Zvvk8BAkyYafpOS/2gFC4F/9v/25Q0=";

        public static async Task AddReportedProperties(string deviceName,ReportedProperties reportedProperties)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Please give valid device name");
            }

            else
            {
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection twinCollection, connectivity;
                twinCollection= new TwinCollection();
                connectivity= new TwinCollection();
                connectivity["type"] = "cellular";
                twinCollection["connectivity"] = "connectivity";
                twinCollection["temperature"] = reportedProperties.temperature;
               // twinCollection["drift"] = reportedProperties.drift;
                //twinCollection["fullscale"] = reportedProperties.fullscale;
               // twinCollection["pressure"] = reportedProperties.pressure;
               // twinCollection["accurarcy"] = reportedProperties.accurarcy;
               // twinCollection["sensorType"] = reportedProperties.sensorType;
               // twinCollection["resolution"] = reportedProperties.resolution;
              //  twinCollection["supplyVoltageLevel"] = reportedProperties.supplyVoltageLevel;
                twinCollection["frequency"] = reportedProperties.frequency;
                twinCollection["dateTimeLastAppLaunch"] = reportedProperties.dateTimeLastAppLaunch;
               await client.UpdateReportedPropertiesAsync(twinCollection);
                return;

            }

        }

        public static async Task DesiredProperties(string deviceName)
        {
            client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device = await registryManager.GetTwinAsync(deviceName);
            TwinCollection twinCollection, telemetryconfig;
            twinCollection = new TwinCollection();
            telemetryconfig = new TwinCollection();
            telemetryconfig["frequency"] = "15Hz";
            twinCollection["telemetryconfig"] = telemetryconfig;
            device.Properties.Desired["telemetryconfig"] = telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId,device,device.ETag);
            return;


        }

        public static async  Task<Twin> GetDeviceProperties(string deviceName)
        {
            var device = await registryManager.GetTwinAsync(deviceName);
            return device;
        }

        public static async Task UpdateDeviceTagProperties(string deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Please give valid device name");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceName);
                var patch =
                    @"{
                       tags:{
                            location:{
                                region:'India',
                                plant:'IOTPro'
                                }
                            }
                    }";
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity =  new TwinCollection();
                connectivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);

            }

        }
    }
}
