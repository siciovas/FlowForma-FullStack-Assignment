using Products.Domain.Enums;

namespace Products.Core.Dto
{
    public class ClothesDto : ProductDto
    {
        public Size Size { get; set; }
        public List<string> Materials { get; set; }
    }
}
