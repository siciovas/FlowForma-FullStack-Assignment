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
            return await _db.Foods.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Food>> GetAll()
        {
            var foods = await _db.Foods.ToListAsync();

            return foods;
        }

        public async Task<Food> Update(FoodDto food, int id)
        {
            var foodUpdate = await _db.Foods
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            foodUpdate.Name = food.Name;
            foodUpdate.Description = food.Description;
            foodUpdate.Price = food.Price;
            foodUpdate.Size = food.Size;
            foodUpdate.Ingredients = food.Ingredients;
            foodUpdate.Calories = food.Calories;
            foodUpdate.IsVegeterian = food.IsVegeterian;
            foodUpdate.IsVegan = food.IsVegan;

            await _db.SaveChangesAsync();

            return foodUpdate;
        }
    }
}
