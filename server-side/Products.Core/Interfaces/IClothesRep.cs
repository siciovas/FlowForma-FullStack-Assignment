using Products.Core.Dto;
using Products.Domain.Entities;

namespace Products.Core.Interfaces
{
    public interface IClothesRep
    {
        Task<List<Clothes>> GetAll();
        Task<Clothes?> Get(int id);
        Task<Clothes> Create(Clothes clothes);
        Task<Clothes> Update(ClothesDto clothes, int id);
        Task Delete(Clothes clothes);
    }
}
