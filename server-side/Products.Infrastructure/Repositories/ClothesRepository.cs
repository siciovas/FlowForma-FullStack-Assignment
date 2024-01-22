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
            return await _db.Clothes.Include(x => x.Materials).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ClothesDto>> GetAll()
        {
            var clothes = await _db.Clothes.Include(x => x.Materials).Select(x => new ClothesDto { 
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Size = x.Size,
            Materials = x.Materials.Select(x => x.Name).ToList()
            }).ToListAsync();

            return clothes;
        }
        
        public async Task<Clothes> Update(ClothesDto clothes, int id)
        {
            var clothesUpdate = await _db.Clothes
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            var existingMaterials = _db.Materials.Where(x => clothes.Materials.Select(material => material).Contains(x.Name)).ToList();

            clothesUpdate.Name = clothes.Name;
            clothesUpdate.Description = clothes.Description;
            clothesUpdate.Price = clothes.Price;
            clothesUpdate.Size = clothes.Size;
            clothesUpdate.Materials = existingMaterials;

            await _db.SaveChangesAsync();

            return clothesUpdate;
        }
    }
}
