using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IoT_Device.Repository;
using Microsoft.Azure.Devices;

namespace IoT_Device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IotDeviceController : ControllerBase
    {
       


        [HttpPost("AddIoTDevice")]
        public async Task<string> Post(string deviceName)
        {
            await IotDevice.AddDevice(deviceName);
                return null;
        }

        [HttpGet("GetIotDevice")]
        public async Task<Device> Get(string deviceName)
        {
            Device device = await IotDevice.GetDevice(deviceName);
            return device;
        }

        [HttpPut("UpdateIoTDevice")]
        public async Task<Device> Put(string deviceName)
        {
            Device device;
            device = await IotDevice.UpdateDevice(deviceName);
            return device;
        }

        [HttpDelete("DeleteIotDevice")]
        public async Task<string> Delete(string deviceName)
        {
            await IotDevice.DeleteDevice(deviceName);
            return null;
        }
    }
}
