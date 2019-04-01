namespace TheProject.Core.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductVariation")]
    public class ProductVariation : Entity<int>
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal DiscountPrice { get; set; }
        [Required]
        public string DeliveredIn { get; set; }
        [Required]
        public string Q1 { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int SizeId { get; set; }
        public Size Size { get; set; }
        [Required]
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
