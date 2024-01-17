namespace Products.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<Food> Foods { get; set; }
    }
}
