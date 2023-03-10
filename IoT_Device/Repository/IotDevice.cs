using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;


namespace IoT_Device.Repository
{
    public class IotDevice
    {
        public static RegistryManager registryManager;

        private static string connectionString = "HostName=pruthviiothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=kj1ZOQffdfRT4TGGuujW0U6S+bjbLr3aAlIRoDZdCxo=";


        public static async Task AddDevice(string deviceName)
        {
            Device device;
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Please add Device Name:");
            }
            registryManager= RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.AddDeviceAsync(new Device(deviceName));
            return ;
        }

        public static async Task<Device> GetDevice(string deviceId)
        {
            Device device;
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device= await registryManager.GetDeviceAsync(deviceId);
            return device;
        }

        public static async Task<Device> UpdateDevice(string deviceId)
        {
            if(string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("Please Enter Device Id:");
            }
            Device device =  new Device(deviceId);
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason = "Deivice Updated";
            device = await registryManager.UpdateDeviceAsync(device);
            return device;

        }

        public static async Task DeleteDevice(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceId);
        }
    }
}
