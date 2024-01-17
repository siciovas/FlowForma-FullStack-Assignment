namespace Products.Domain.Entities
{
    public abstract class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Price { get; set; }
    }
}
