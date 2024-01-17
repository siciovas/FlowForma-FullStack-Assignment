using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Database;

namespace Products.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRep
    {
        private readonly DatabaseContext _db;

        public DeviceRepository(DatabaseContext db) { _db = db; }

        public async Task<Device> Create(Device device)
        {
            _db.Devices.Add(device);
            await _db.SaveChangesAsync();

            return device;
        }

        public async Task Delete(Device device)
        {
            _db.Devices.Remove(device);
            await _db.SaveChangesAsync();
        }

        public async Task<Device?> Get(int id)
        {
            return await _db.Devices.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Device>> GetAll()
        {
            var devices = await _db.Devices.ToListAsync();

            return devices;
        }

        public async Task<Device> Update(DeviceDto device, int id)
        {
            var deviceUpdate = await _db.Devices
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            deviceUpdate.Name = device.Name;
            deviceUpdate.Description = device.Description;
            deviceUpdate.Price = device.Price;
            deviceUpdate.IsElectronical = device.IsElectronical;

            await _db.SaveChangesAsync();

            return deviceUpdate;
        }
    }
}
