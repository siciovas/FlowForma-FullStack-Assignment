using Products.Core.Dto;
using Products.Domain.Entities;

namespace Products.Core.Interfaces
{
    public interface IDeviceRep
    {
        Task<List<Device>> GetAll();
        Task<Device?> Get(int id);
        Task<Device> Create(Device device);
        Task<Device> Update(DeviceDto device, int id);
        Task Delete(Device device);
    }
}
