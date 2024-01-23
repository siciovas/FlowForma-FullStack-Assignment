using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Domain.Entities;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;

namespace Product.Test
{
    public class IngredientRepositoryTests
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public IngredientRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _databaseContext = new DatabaseContext(dbContextOptions);
            _fixture = new Fixture();

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private IngredientRepository CreateRepository()
        {
            return new IngredientRepository(_databaseContext);
        }

        [Fact]
        public async void GetAll_GetAllIngredients_ReturnsCorrectData()
        {
            var ingredientRepository = CreateRepository();

            var ingredient = _fixture
                .Create<Ingredient>();

            await _databaseContext.AddAsync(ingredient);
            await _databaseContext.SaveChangesAsync();

            var result = await ingredientRepository.GetAll();

            Assert.Single(result);
            Assert.Equal(ingredient.Name, result[0].Name);
        }

        [Fact]
        public async void Get_GetIngredient_ReturnsCorrectData()
        {
            var ingredientRepository = CreateRepository();

            var ingredient = _fixture
                .Create<Ingredient>();

            await _databaseContext.AddAsync(ingredient);
            await _databaseContext.SaveChangesAsync();

            var result = await ingredientRepository.Get(ingredient.Id);

            Assert.NotNull(ingredient);
            Assert.Equal(ingredient.Name, result!.Name);
        }

        [Fact]
        public async void Create_CreateIngredient_ReturnsCorrectData()
        {
            var ingredientRepository = CreateRepository();

            var ingredient = _fixture
                .Create<Ingredient>();

            await ingredientRepository.Create(ingredient);

            var expected = await _databaseContext.Ingredients.FirstAsync();

            Assert.Equal(expected.Name, ingredient.Name);
        }

        [Fact]
        public async void Update_UpdateIngredient_ReturnsCorrectData()
        {
            var ingredientRepository = CreateRepository();

            var ingredient = _fixture
                .Create<Ingredient>();

            await _databaseContext.AddAsync(ingredient);
            await _databaseContext.SaveChangesAsync();

            var newName = _fixture.Create<string>();

            var ingredientDto = new IngredientDto
            {
                Name = newName
            };

            await ingredientRepository.Update(ingredientDto, ingredient.Id);

            var expected = await _databaseContext.Ingredients.FirstAsync();

            Assert.Equal(expected.Name, newName);
        }

        [Fact]
        public async void Delete_DeletesIngredient_ReturnsCorrectData()
        {
            var ingredientRepository = CreateRepository();

            var ingredient = _fixture
                .Create<Ingredient>();

            await _databaseContext.AddAsync(ingredient);
            await _databaseContext.SaveChangesAsync();

            await ingredientRepository.Delete(ingredient);

            var expected = await _databaseContext.Ingredients.FirstOrDefaultAsync();

            Assert.Null(expected);
        }
    }
}
