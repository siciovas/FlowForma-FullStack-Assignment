using Microsoft.EntityFrameworkCore;
using Products.Core.Dto;
using Products.Core.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Database;

namespace Products.Infrastructure.Repositories
{
    public class MaterialRepository : IMaterialRep
    {
        private readonly DatabaseContext _db;

        public MaterialRepository(DatabaseContext db) { _db = db; }

        public async Task<Material> Create(Material material)
        {
            _db.Materials.Add(material);
            await _db.SaveChangesAsync();

            return material;
        }

        public async Task Delete(Material material)
        {
            _db.Materials.Remove(material);
            await _db.SaveChangesAsync();
        }

        public Task<Material?> Get(int id)
        {
            return _db.Materials.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Material>> GetAll()
        {
            var materials = await _db.Materials.ToListAsync();

            return materials;
        }

        public async Task<Material> Update(MaterialDto material, int id)
        {
            var materialUpdate = await _db.Materials
                .AsTracking()
                .FirstAsync(x => x.Id == id);

            materialUpdate.Name = material.Name;
            
            await _db.SaveChangesAsync();

            return materialUpdate;

        }
    }
}
