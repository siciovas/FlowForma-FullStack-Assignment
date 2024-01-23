using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Domain.Entities;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;

namespace Product.Test
{
    public class MaterialRepositoryTests
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public MaterialRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _databaseContext = new DatabaseContext(dbContextOptions);
            _fixture = new Fixture();

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private MaterialRepository CreateRepository()
        {
            return new MaterialRepository(_databaseContext);
        }

        [Fact]
        public async void GetAll_GetAllMaterials_ReturnsCorrectData()
        {
            var materialRepository = CreateRepository();

            var material = _fixture
                .Create<Material>();

            await _databaseContext.AddAsync(material);
            await _databaseContext.SaveChangesAsync();

            var result = await materialRepository.GetAll();

            Assert.Single(result);
            Assert.Equal(material.Name, result[0].Name);
        }

        [Fact]
        public async void Get_GetMaterial_ReturnsCorrectData()
        {
            var materialRepository = CreateRepository();

            var material = _fixture
                .Create<Material>();

            await _databaseContext.AddAsync(material);
            await _databaseContext.SaveChangesAsync();

            var result = await materialRepository.Get(material.Id);

            Assert.NotNull(material);
            Assert.Equal(material.Name, result!.Name);
        }

        [Fact]
        public async void Create_CreateMaterial_ReturnsCorrectData()
        {
            var materialRepository = CreateRepository();

            var material = _fixture
                .Create<Material>();

            await materialRepository.Create(material);

            var expected = await _databaseContext.Materials.FirstAsync();

            Assert.Equal(expected.Name, material.Name);
        }

        [Fact]
        public async void Update_UpdateMaterial_ReturnsCorrectData()
        {
            var materialRepository = CreateRepository();

            var material = _fixture
                .Create<Material>();

            await _databaseContext.AddAsync(material);
            await _databaseContext.SaveChangesAsync();

            var newName = _fixture.Create<string>();

            var materialDto = new MaterialDto
            {
                Name = newName
            };

            await materialRepository.Update(materialDto, material.Id);

            var expected = await _databaseContext.Materials.FirstAsync();

            Assert.Equal(expected.Name, newName);
        }

        [Fact]
        public async void Delete_DeletesMaterial_ReturnsCorrectData()
        {
            var materialRepository = CreateRepository();

            var material = _fixture
                .Create<Material>();

            await _databaseContext.AddAsync(material);
            await _databaseContext.SaveChangesAsync();

            await materialRepository.Delete(material);

            var expected = await _databaseContext.Materials.FirstOrDefaultAsync();

            Assert.Null(expected);
        }
    }
}
