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
    public class TelemetryController : ControllerBase
    {

        

        [HttpPost("SendTelemeteryMessage")]
        public async Task<string> SendMessage(string deviceName)
        {
            await SendTelemeteryMessages.SendMessage(deviceName);
            return null;
        }
    }
}
