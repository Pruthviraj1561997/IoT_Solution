using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IoT_Device.Models;
using IoT_Device.Repository;
using Microsoft.Azure.Devices.Shared;


namespace IoT_Device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IotDevicePropertiesController : ControllerBase
    {

       

        [HttpPut("UpdateIotDeviceReportProperties")]
        public async Task<string> UpdateReportProperties(string deviceName,ReportedProperties reportedProperties)
        {
            await IoTDeviceProperties.AddReportedProperties(deviceName, reportedProperties);
            return null;

        }

        [HttpPut("UpdateIotDeviceDesiredProperties")]
        public async Task<string> UpdateDesiredProperties(string deviceName)
        {
            await IoTDeviceProperties.DesiredProperties(deviceName);
            return null;

        }

        [HttpPut("UpdateIotDeviceTagProperties")]
        public async Task<string> UpdateTagProperties(string deviceName)
        {
            await IoTDeviceProperties.UpdateDeviceTagProperties(deviceName);
            return null;

        }

        [HttpGet("GetIoTDeviceProperties")]
        public async Task<Twin> GetIoTDevice(string deviceName)
        {
           var device= await IoTDeviceProperties.GetDeviceProperties(deviceName);
            return device;

        }
    }
}
