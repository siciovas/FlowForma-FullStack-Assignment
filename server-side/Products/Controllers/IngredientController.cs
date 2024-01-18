using Microsoft.AspNetCore.Mvc;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/ingredient")]
    public class IngredientController : Controller
    {
        private readonly IIngredientRep _ingredientRep;

        public IngredientController(IIngredientRep ingredientRep)
        {
            _ingredientRep = ingredientRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> GetAll()
        {
            var ingredient = await _ingredientRep.GetAll();

            return Ok(ingredient);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Ingredient>> Get(int id)
        {
            var ingredient = await _ingredientRep.Get(id);

            if (ingredient == null) return NotFound();

            return Ok(ingredient);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> Delete(int id)
        {
            var ingredient = await _ingredientRep.Get(id);

            if (ingredient == null) return NotFound();

            await _ingredientRep.Delete(ingredient);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Create(IngredientDto ingredient)
        {
            var newIngredient = new Ingredient
            {
                Name = ingredient.Name,
            };

            var createdIngredient = await _ingredientRep.Create(newIngredient);

            return CreatedAtAction(nameof(Get), new { id = createdIngredient.Id }, createdIngredient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IngredientDto>> Update(IngredientDto ingredient, int id)
        {
            var updateIngredient = await _ingredientRep.Get(id);

            if (updateIngredient == null) return NotFound();

            await _ingredientRep.Update(ingredient, id);

            return Ok(ingredient);
        }
    }
}
