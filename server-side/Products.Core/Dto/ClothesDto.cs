using Products.Domain.Entities;
using Products.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Dto
{
    public class ClothesDto : ProductDto
    {
        public Size Size { get; set; }
        public List<string> Materials { get; set; }
    }
}
