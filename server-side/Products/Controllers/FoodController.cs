using Microsoft.AspNetCore.Mvc;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/food")]
    public class FoodController : Controller
    {
        private readonly IFoodRep _foodRep;

        public FoodController(IFoodRep foodRep)
        {
            _foodRep = foodRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<FoodDto>>> GetAll()
        {
            var food = await _foodRep.GetAll();

            return Ok(food);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FoodDto>> Get(int id)
        {
            var food = await _foodRep.Get(id);

            if (food == null) return NotFound();

            return Ok(new FoodDto
            {
                Id = food.Id,
                Description = food.Description,
                Name = food.Name,
                Calories = food.Calories,
                Ingredients = food.Ingredients.Select(ingredient => ingredient.Name).ToList(),
                IsVegan = food.IsVegan,
                IsVegetarian = food.IsVegetarian,
                Price = food.Price,
                Size = food.Size
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> Delete(int id)
        {
            var food = await _foodRep.Get(id);

            if (food == null) return NotFound();

            await _foodRep.Delete(food);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<FoodDto>> Create(FoodDto food)
        {
            var newFood = new Food
            {
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                Size = food.Size,
                Calories = food.Calories,
                Ingredients = food.Ingredients.Select(ingredient => new Ingredient { Name = ingredient }).ToList(),
                IsVegetarian = food.IsVegetarian,
                IsVegan = food.IsVegan
            };

            var createdFood = await _foodRep.Create(newFood);

            return CreatedAtAction(nameof(Get), new { id = createdFood.Id }, createdFood);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FoodDto>> Update(FoodDto food, int id)
        {
            var updateFood = await _foodRep.Get(id);

            if (updateFood == null) return NotFound();

            await _foodRep.Update(food, id);

            return Ok(food);
        }
    }
}
