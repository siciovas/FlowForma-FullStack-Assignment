using Products.Core.Dto;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
