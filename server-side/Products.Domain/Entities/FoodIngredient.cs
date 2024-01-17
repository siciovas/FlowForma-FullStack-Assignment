using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Entities
{
    public class FoodIngredient
    {
        public int FoodId { get; set; }
        public int IngredientId { get; set; }
        public required Food Food { get; set; }
        public required Ingredient Ingredient { get; set; }
    }
}
