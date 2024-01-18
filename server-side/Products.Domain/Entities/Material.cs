namespace Products.Domain.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Clothes> Clothes { get; set; } = [];
    }
}
