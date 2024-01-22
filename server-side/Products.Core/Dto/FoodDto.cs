using Products.Domain.Entities;
using Products.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Dto
{
    public class FoodDto : ProductDto
    {
        public Size Size { get; set; }
        public List<string> Ingredients { get; set; }
        public double Calories { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
    }
}
