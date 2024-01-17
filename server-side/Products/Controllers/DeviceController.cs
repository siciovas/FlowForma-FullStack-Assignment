using Microsoft.AspNetCore.Mvc;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : Controller
    {
        private readonly IDeviceRep _deviceRep;

        public DeviceController(IDeviceRep deviceRep)
        {
            _deviceRep = deviceRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetAll()
        {
            var device = await _deviceRep.GetAll();

            return Ok(device);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Device>> Get(int id)
        {
            var device = await _deviceRep.Get(id);

            if (device == null) return NotFound();

            return Ok(device);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Device>> Delete(int id)
        {
            var device = await _deviceRep.Get(id);

            if (device == null) return NotFound();

            await _deviceRep.Delete(device);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DeviceDto>> Create(DeviceDto device)
        {
            var newDevice = new Device
            {
                Name = device.Name,
                Description = device.Description,
                Price = device.Price,
                IsElectronical = device.IsElectronical,
                
            };

            var createdDevice = await _deviceRep.Create(newDevice);

            return CreatedAtAction(nameof(Get), new { id = createdDevice.Id }, createdDevice);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceDto>> Update(DeviceDto device, int id)
        {
            var updateDevice = await _deviceRep.Get(id);

            if (updateDevice == null) return NotFound();

            await _deviceRep.Update(device, id);

            return Ok(device);
        }
    }
}
