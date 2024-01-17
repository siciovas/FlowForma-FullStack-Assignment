using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Database;

namespace Products.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRep
    {
        private readonly DatabaseContext _db;

        public IngredientRepository(DatabaseContext db) { _db = db; }

        public async Task<Ingredient> Create(Ingredient composition)
        {
            _db.Ingredients.Add(composition);
            await _db.SaveChangesAsync();

            return composition;
        }

        public async Task Delete(Ingredient composition)
        {
            _db.Ingredients.Remove(composition);
            await _db.SaveChangesAsync();
        }

        public Task<Ingredient?> Get(int id)
        {
            return _db.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Ingredient>> GetAll()
        {
            var compositions = await _db.Ingredients.ToListAsync();

            return compositions;
        }

        public async Task<Ingredient> Update(IngredientDto composition, int id)
        {
            var compositionUpdate = await _db.Ingredients
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            compositionUpdate.Name = composition.Name;

            await _db.SaveChangesAsync();

            return compositionUpdate;

        }
    }
}
