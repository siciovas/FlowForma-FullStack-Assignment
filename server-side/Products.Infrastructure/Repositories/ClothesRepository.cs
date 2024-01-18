using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Database;

namespace Products.Infrastructure.Repositories
{
    public class ClothesRepository : IClothesRep
    {
        private readonly DatabaseContext _db;

        public ClothesRepository(DatabaseContext db) {  _db = db; }

        public async Task<Clothes> Create(Clothes clothes)
        {
            var existingMaterials = _db.Materials.Where(x => clothes.Materials.Select(x => x.Name).Contains(x.Name)).ToList();

            clothes.Materials = existingMaterials;

            _db.Clothes.Add(clothes);
            await _db.SaveChangesAsync();

            return clothes;
        }

        public async Task Delete(Clothes clothes)
        {
            _db.Clothes.Remove(clothes);
            await _db.SaveChangesAsync();
        }

        public async Task<Clothes?> Get(int id)
        {
            return await _db.Clothes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Clothes>> GetAll()
        {
            var clothes = await _db.Clothes.ToListAsync();

            return clothes;
        }
        
        public async Task<Clothes> Update(ClothesDto clothes, int id)
        {
            var clothesUpdate = await _db.Clothes
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            clothesUpdate.Name = clothes.Name;
            clothesUpdate.Description = clothes.Description;
            clothesUpdate.Price = clothes.Price;
            clothesUpdate.Size = clothes.Size;
            clothesUpdate.Materials = clothes.Materials.Select(x => new Material { Name = x.Name }).ToList();

            await _db.SaveChangesAsync();

            return clothesUpdate;
        }
    }
}
