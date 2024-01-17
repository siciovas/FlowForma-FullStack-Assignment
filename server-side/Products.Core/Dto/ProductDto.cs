using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Dto
{
    public abstract class ProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Price { get; set; }
    }
}
