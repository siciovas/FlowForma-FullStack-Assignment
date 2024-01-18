using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;
using Products.Domain.Entities;
using Products.Core.Dto;

namespace Product.Test
{
    public class DeviceRepositoryTests
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public DeviceRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _databaseContext = new DatabaseContext(dbContextOptions);
            _fixture = new Fixture();

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private DeviceRepository CreateRepository()
        {
            return new DeviceRepository(_databaseContext);
        }

        [Fact]
        public async void GetAll_GetAllDevices_ReturnsCorrectData()
        {
            var deviceRepository = CreateRepository();

            var device = _fixture
                .Create<Device>();

            await _databaseContext.AddAsync(device);
            await _databaseContext.SaveChangesAsync();

            var result = await deviceRepository.GetAll();

            Assert.Single(result);
            Assert.Equal(device.Name, result[0].Name);
            Assert.Equal(device.Description, result[0].Description);
            Assert.Equal(device.Price, result[0].Price);
            Assert.Equal(device.IsElectronical, result[0].IsElectronical);
        }

        [Fact]
        public async void Get_GetDevice_ReturnsCorrectData()
        {
            var deviceRepository = CreateRepository();

            var device = _fixture
                .Create<Device>();

            await _databaseContext.AddAsync(device);
            await _databaseContext.SaveChangesAsync();

            var result = await deviceRepository.Get(device.Id);

            Assert.Equal(device.Name, result.Name);
            Assert.Equal(device.Description, result.Description);
            Assert.Equal(device.Price, result.Price);
            Assert.Equal(device.IsElectronical, result.IsElectronical);
        }

        [Fact]
        public async void Create_CreateDevice_ReturnsCorrectData()
        {
            var deviceRepository = CreateRepository();

            var device = _fixture
                .Create<Device>();

            var result = await deviceRepository.Create(device);

            var expected = await _databaseContext.Devices.FirstAsync();

            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Description, result.Description);
            Assert.Equal(expected.Price, result.Price);
            Assert.Equal(expected.IsElectronical, result.IsElectronical);
        }

        [Fact]
        public async void Update_UpdateDevice_ReturnsCorrectData()
        {
            var deviceRepository = CreateRepository();

            var device = _fixture
                .Create<Device>();

            await _databaseContext.AddAsync(device);
            await _databaseContext.SaveChangesAsync();

            var newName = _fixture.Create<string>();
            var newDescription = _fixture.Create<string>();

            var deviceDto = new DeviceDto
            {
                Name = newName,
                Description = newDescription
            };

            await deviceRepository.Update(deviceDto, device.Id);

            var expected = await _databaseContext.Devices.FirstAsync();
            
            Assert.Equal(expected.Name, newName);
            Assert.Equal(expected.Description, newDescription);
        }

        [Fact]
        public async void Delete_DeletesDevice_ReturnsCorrectData()
        {
            var deviceRepository = CreateRepository();

            var device = _fixture
                .Create<Device>();

            await _databaseContext.AddAsync(device);
            await _databaseContext.SaveChangesAsync();

            await deviceRepository.Delete(device);

            var expected = await _databaseContext.Devices.FirstOrDefaultAsync();

            Assert.Null(expected);
        }
    }
}