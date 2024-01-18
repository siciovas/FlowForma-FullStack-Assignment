namespace Products.Domain.Entities
{
    public class ClothesMaterial
    {
        public int ClothesId { get; set; }
        public int MaterialId { get; set; }
        public required Clothes Clothes { get; set; }
        public required Material Material { get; set; }
    }
}
