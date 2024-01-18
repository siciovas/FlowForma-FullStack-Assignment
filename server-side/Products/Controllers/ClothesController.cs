using Microsoft.AspNetCore.Mvc;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/clothes")]
    public class ClothesController : Controller
    {
        private readonly IClothesRep _clothesRep;

        public ClothesController(IClothesRep clothesRep)
        {
            _clothesRep = clothesRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clothes>>> GetAll()
        {
            var clothes = await _clothesRep.GetAll();

            return Ok(clothes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Clothes>> Get(int id)
        {
            var clothes = await _clothesRep.Get(id);

            if (clothes == null) return NotFound();

            return Ok(clothes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Clothes>> Delete(int id)
        {
            var clothes = await _clothesRep.Get(id);

            if (clothes == null) return NotFound();

            await _clothesRep.Delete(clothes);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ClothesDto>> Create(ClothesDto clothes)
        {
            var newClothes = new Clothes
            {
                Name = clothes.Name,
                Description = clothes.Description,
                Price = clothes.Price,
                Size = clothes.Size,
                Materials = clothes.Materials.Select(x => new Material { Name = x.Name}).ToList()
            };

            var createdClothes = await _clothesRep.Create(newClothes);

            return CreatedAtAction(nameof(Get), new { id = createdClothes.Id }, createdClothes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClothesDto>> Update(ClothesDto clothes, int id)
        {
            var updateClothes = await _clothesRep.Get(id);

            if (updateClothes == null) return NotFound();

            await _clothesRep.Update(clothes, id);

            return Ok(clothes);
        }
    }
}
