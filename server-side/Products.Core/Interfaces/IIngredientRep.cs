using Products.Core.Dto;
using Products.Domain.Entities;

namespace Products.Core.Interfaces
{
    public interface IIngredientRep
    {
        Task<List<Ingredient>> GetAll();
        Task<Ingredient?> Get(int id);
        Task<Ingredient> Create(Ingredient composition);
        Task<Ingredient> Update(IngredientDto composition, int id);
        Task Delete(Ingredient composition);
    }
}
