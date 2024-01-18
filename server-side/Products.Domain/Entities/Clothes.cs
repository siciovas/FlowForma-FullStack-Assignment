using Products.Domain.Enums;

namespace Products.Domain.Entities
{
    public class Clothes : Product
    {
        public Size Size { get; set; }
        public List<Material> Materials { get; set; } = [];
    }
}
