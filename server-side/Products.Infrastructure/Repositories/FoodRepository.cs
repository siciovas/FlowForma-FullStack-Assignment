using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Database;

namespace Products.Infrastructure.Repositories
{
    public class FoodRepository : IFoodRep
    {
        private readonly DatabaseContext _db;

        public FoodRepository(DatabaseContext db) { _db = db; }

        public async Task<Food> Create(Food food)
        {
            var existingIngredients = _db.Ingredients.Where(x => food.Ingredients.Select(x => x.Name).Contains(x.Name)).ToList();

            food.Ingredients = existingIngredients;

            _db.Foods.Add(food);
            await _db.SaveChangesAsync();

            return food;
        }

        public async Task Delete(Food food)
        {
            _db.Foods.Remove(food);
            await _db.SaveChangesAsync();
        }

        public async Task<Food?> Get(int id)
        {
            return await _db.Foods.Include(x => x.Ingredients).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<FoodDto>> GetAll()
        {
            var foods = await _db.Foods.Include(x => x.Ingredients).Select(x => new FoodDto {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Size = x.Size,
                Price = x.Price,
                Calories = x.Calories,
                Ingredients = x.Ingredients.Select(x => x.Name).ToList(),
                IsVegan = x.IsVegan,
                IsVegetarian = x.IsVegetarian,
            }).ToListAsync();

            return foods;
        }

        public async Task<Food> Update(FoodDto food, int id)
        {
            var foodUpdate = await _db.Foods
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            var existingIngredients = _db.Ingredients.Where(x => food.Ingredients.Select(ingredient => ingredient).Contains(x.Name)).ToList();

            foodUpdate.Name = food.Name;
            foodUpdate.Description = food.Description;
            foodUpdate.Price = food.Price;
            foodUpdate.Size = food.Size;
            foodUpdate.Ingredients = existingIngredients;
            foodUpdate.Calories = food.Calories;
            foodUpdate.IsVegetarian = food.IsVegetarian;
            foodUpdate.IsVegan = food.IsVegan;

            await _db.SaveChangesAsync();

            return foodUpdate;
        }
    }
}
