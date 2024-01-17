using Products.Domain.Enums;

namespace Products.Domain.Entities
{
    public class Food : Product
    {
        public Size Size { get; set; }
        public required List<Ingredient> Ingredients { get; set; }
        public double Calories { get; set; }
        public bool IsVegeterian { get; set; }
        public bool IsVegan { get; set; }
    }
}
