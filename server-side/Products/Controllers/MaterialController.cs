using Microsoft.AspNetCore.Mvc;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/material")]
    public class MaterialController : Controller
    {
        private readonly IMaterialRep _materialRep;

        public MaterialController(IMaterialRep materialRep)
        {
            _materialRep = materialRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<Material>>> GetAll()
        {
            var material = await _materialRep.GetAll();

            return Ok(material);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Material>> Get(int id)
        {
            var material = await _materialRep.Get(id);

            if (material == null) return NotFound();

            return Ok(material);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> Delete(int id)
        {
            var material = await _materialRep.Get(id);

            if (material == null) return NotFound();

            await _materialRep.Delete(material);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MaterialDto>> Create(MaterialDto material)
        {
            var newMaterial = new Material
            {
                Name = material.Name,
            };

            var createdIngredient = await _materialRep.Create(newMaterial);

            return CreatedAtAction(nameof(Get), new { id = newMaterial.Id }, newMaterial);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaterialDto>> Update(MaterialDto material, int id)
        {
            var updateMaterial = await _materialRep.Get(id);

            if (updateMaterial == null) return NotFound();

            await _materialRep.Update(material, id);

            return Ok(material);
        }
    }
}
