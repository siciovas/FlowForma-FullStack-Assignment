using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Entities
{
    public class FoodIngredient
    {
        public int FoodsId { get; set; }
        public int IngredientsId { get; set; }

        public Food Food { get; set; } = null!;

        public Ingredient Ingredient { get; set; } = null!;
    }
}
