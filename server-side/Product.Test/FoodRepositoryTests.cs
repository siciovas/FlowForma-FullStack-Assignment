using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;

namespace Product.Test
{
    public class FoodRepositoryTests
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public FoodRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _databaseContext = new DatabaseContext(dbContextOptions);
            _fixture = new Fixture();

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private FoodRepository CreateRepository()
        {
            return new FoodRepository(_databaseContext);
        }

        [Fact]
        public async void GetAll_GetAllFoods_ReturnsCorrectData()
        {
            var foodRepository = CreateRepository();

            var ingredient = _fixture
                .Build<Ingredient>()
                .Without(x => x.Foods)
                .Create();

            var food = _fixture
                .Build<Food>()
                .With(x => x.Ingredients, [ingredient])
                .Create();

            await _databaseContext.AddRangeAsync(ingredient, food);
            await _databaseContext.SaveChangesAsync();

            var result = await foodRepository.GetAll();

            Assert.Single(result);
            Assert.Equal(food.Name, result[0]!.Name);
            Assert.Equal(food.Description, result[0].Description);
            Assert.Equal(food.Price, result[0].Price);
            Assert.Equal(food.Ingredients[0].Name, result[0].Ingredients.First());
        }

        [Fact]
        public async void Get_GetFood_ReturnsCorrectData()
        {
            var foodRepository = CreateRepository();

            var ingredient = _fixture
                .Build<Ingredient>()
                .Without(x => x.Foods)
                .Create();

            var food = _fixture
                .Build<Food>()
                .With(x => x.Ingredients, [ingredient])
                .Create();

            await _databaseContext.AddRangeAsync(ingredient, food);
            await _databaseContext.SaveChangesAsync();

            var result = await foodRepository.Get(food.Id);

            Assert.NotNull(result);
            Assert.Equal(food.Name, result!.Name);
            Assert.Equal(food.Description, result.Description);
            Assert.Equal(food.Price, result.Price);
            Assert.Equal(food.Ingredients[0].Name, ingredient.Name);
        }

        [Fact]
        public async void Delete_DeletesFood_ReturnsCorrectData()
        {
            var foodRepository = CreateRepository();

            var ingredient = _fixture
                .Build<Ingredient>()
                .Without(x => x.Foods)
                .Create();

            var food = _fixture
                .Build<Food>()
                .With(x => x.Ingredients, [ingredient])
                .Create();

            await _databaseContext.AddRangeAsync(ingredient, food);
            await _databaseContext.SaveChangesAsync();

            await foodRepository.Delete(food);

            var expected = await _databaseContext.Clothes.FirstOrDefaultAsync();

            Assert.Null(expected);
        }
    }
}
