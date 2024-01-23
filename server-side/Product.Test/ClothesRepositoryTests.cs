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
        public async void Create_CreateClothes_ReturnsCorrectData()
        {
            var clothesRepository = CreateRepository();

            var material = _fixture
                .Build<Material>()
                .Without(x => x.Clothes)
                .Without(x => x.Id)
                .Create();

            var clothes = _fixture
                .Build<Clothes>()
                .With(x => x.Materials, [material])
                .Without(x => x.Id)
                .Create();

            await _databaseContext.AddAsync(material);
            await _databaseContext.SaveChangesAsync();

            await clothesRepository.Create(clothes);

            var expected = await _databaseContext.Clothes.FirstAsync();

            Assert.Equal(expected.Name, clothes.Name);
            Assert.Equal(expected.Materials[0].Name, material.Name);
        }

        [Fact]
        public async void Update_UpdateClothes_ReturnsCorrectData()
        {
            var clothesRepository = CreateRepository();

            var materials = _fixture
                .Build<Material>()
                .Without(x => x.Clothes)
                .Without(x => x.Id)
                .CreateMany(2).ToList();

            var clothes = _fixture
                .Build<Clothes>()
                .With(x => x.Materials, [materials[0]])
                .Without(x => x.Id)
                .Create();

            await _databaseContext.AddAsync(clothes);
            await _databaseContext.AddRangeAsync(materials);
            await _databaseContext.SaveChangesAsync();

            var newName = _fixture.Create<string>();
            var newDescription = _fixture.Create<string>();

            var clothesDto = new ClothesDto
            {
                Name = newName,
                Description = newDescription,
                Materials = new List<string> {
                    materials[1].Name
                }
            };

            await clothesRepository.Update(clothesDto, clothes.Id);

            var expected = await _databaseContext.Clothes.FirstAsync();

            Assert.Equal(expected.Name, newName);
            Assert.Equal(expected.Description, newDescription);
            Assert.Equal(expected.Materials[0].Name, materials[1].Name);
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
