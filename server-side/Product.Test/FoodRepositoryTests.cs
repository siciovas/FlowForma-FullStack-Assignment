using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
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
        public async void Create_CreateFood_ReturnsCorrectData()
        {
            var foodRepository = CreateRepository();

            var ingredient = _fixture
                .Build<Ingredient>()
                .Without(x => x.Foods)
                .Without(x => x.Id)
                .Create();

            var food = _fixture
                .Build<Food>()
                .With(x => x.Ingredients, [ingredient])
                .Without(x => x.Id)
                .Create();

            await _databaseContext.AddAsync(ingredient);
            await _databaseContext.SaveChangesAsync();

            await foodRepository.Create(food);

            var expected = await _databaseContext.Foods.FirstAsync();

            Assert.Equal(expected.Name, food.Name);
            Assert.Equal(expected.Ingredients[0].Name, ingredient.Name);
        }

        [Fact]
        public async void Update_UpdateFood_ReturnsCorrectData()
        {
            var foodRepository = CreateRepository();

            var ingredients = _fixture
                .Build<Ingredient>()
                .Without(x => x.Foods)
                .Without(x => x.Id)
                .CreateMany(2).ToList();

            var food = _fixture
                .Build<Food>()
                .With(x => x.Ingredients, [ingredients[0]])
                .Without(x => x.Id)
                .Create();

            await _databaseContext.AddAsync(food);
            await _databaseContext.AddRangeAsync(ingredients);
            await _databaseContext.SaveChangesAsync();

            var newName = _fixture.Create<string>();
            var newDescription = _fixture.Create<string>();

            var foodDto = new FoodDto
            {
                Name = newName,
                Description = newDescription,
                Ingredients = new List<string> {
                    ingredients[1].Name
                }
            };

            await foodRepository.Update(foodDto, food.Id);

            var expected = await _databaseContext.Foods.FirstAsync();

            Assert.Equal(expected.Name, newName);
            Assert.Equal(expected.Description, newDescription);
            Assert.Equal(expected.Ingredients[0].Name, ingredients[1].Name);
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
