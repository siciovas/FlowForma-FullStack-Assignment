using Products.Core.Dto;
using Products.Domain.Entities;

namespace Products.Core.Interfaces
{
    public interface IMaterialRep
    {
        Task<List<Material>> GetAll();
        Task<Material?> Get(int id);
        Task<Material> Create(Material composition);
        Task<Material> Update(MaterialDto composition, int id);
        Task Delete(Material composition);
    }
}
