using Products.Core.Dto;
using Products.Domain.Entities;

namespace Products.Core.Interfaces
{
    public interface IFoodRep
    {
        Task<List<Food>> GetAll();
        Task<Food?> Get(int id);
        Task<Food> Create(Food food);
        Task<Food> Update(FoodDto food, int id);
        Task Delete(Food food);
    }
}
