using Microsoft.AspNetCore.Mvc;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/composition")]
    public class CompositionController : Controller
    {
        private readonly IIngredientRep _compositionRep;

        public CompositionController(IIngredientRep compositionRep)
        {
            _compositionRep = compositionRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> GetAll()
        {
            var composition = await _compositionRep.GetAll();

            return Ok(composition);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Ingredient>> Get(int id)
        {
            var composition = await _compositionRep.Get(id);

            if (composition == null) return NotFound();

            return Ok(composition);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> Delete(int id)
        {
            var composition = await _compositionRep.Get(id);

            if (composition == null) return NotFound();

            await _compositionRep.Delete(composition);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Create(IngredientDto composition)
        {
            var newComposition = new Ingredient
            {
                Name = composition.Name,
            };

            var createdComposition = await _compositionRep.Create(newComposition);

            return CreatedAtAction(nameof(Get), new { id = createdComposition.Id }, createdComposition);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IngredientDto>> Update(IngredientDto composition, int id)
        {
            var updateComposition = await _compositionRep.Get(id);

            if (updateComposition == null) return NotFound();

            await _compositionRep.Update(composition, id);

            return Ok(composition);
        }
    }
}
