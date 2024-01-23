using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Domain.Entities;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;

namespace Product.Test
{
    public class ClothesRepositoryTests
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public ClothesRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _databaseContext = new DatabaseContext(dbContextOptions);
            _fixture = new Fixture();

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private ClothesRepository CreateRepository()
        {
            return new ClothesRepository(_databaseContext);
        }

        [Fact]
        public async void GetAll_GetAllClothes_ReturnsCorrectData()
        {
            var clothesRepository = CreateRepository();

            var material = _fixture
                .Build<Material>()
                .Without(x => x.Clothes)
                .Create();

            var clothes = _fixture
                .Build<Clothes>()
                .With(x => x.Materials, [material])
                .Create();

            await _databaseContext.AddRangeAsync(material, clothes);
            await _databaseContext.SaveChangesAsync();

            var result = await clothesRepository.GetAll();

            Assert.Single(result);
            Assert.Equal(clothes.Name, result[0]!.Name);
            Assert.Equal(clothes.Description, result[0].Description);
            Assert.Equal(clothes.Price, result[0].Price);
            Assert.Equal(clothes.Materials[0].Name, result[0].Materials.First());
        }

        [Fact]
        public async void Get_GetClothes_ReturnsCorrectData()
        {
            var clothesRepository = CreateRepository();

            var material = _fixture
                .Build<Material>()
                .Without(x => x.Clothes)
                .Create();

            var clothes = _fixture
                .Build<Clothes>()
                .With(x => x.Materials, [material])
                .Create();

            await _databaseContext.AddRangeAsync(material, clothes);
            await _databaseContext.SaveChangesAsync();

            var result = await clothesRepository.Get(clothes.Id);

            Assert.NotNull(result);
            Assert.Equal(clothes.Name, result!.Name);
            Assert.Equal(clothes.Description, result.Description);
            Assert.Equal(clothes.Price, result.Price);
            Assert.Equal(clothes.Materials[0].Name, material.Name);
        }

        [Fact]
        public async void Delete_DeletesClothes_ReturnsCorrectData()
        {
            var clothesRepository = CreateRepository();

            var material = _fixture
                .Build<Material>()
                .Without(x => x.Clothes)
                .Create();

            var clothes = _fixture
                .Build<Clothes>()
                .With(x => x.Materials, [material])
                .Create();

            await _databaseContext.AddRangeAsync(material, clothes);
            await _databaseContext.SaveChangesAsync();

            await clothesRepository.Delete(clothes);

            var expected = await _databaseContext.Clothes.FirstOrDefaultAsync();

            Assert.Null(expected);
        }
    }
}
