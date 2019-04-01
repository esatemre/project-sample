namespace TheProject.Application.Products.DTOs
{
    using Core.DTOs;

    public class ProductVariationDto : BaseDto<int>
    {
        public string Key { get; set; }
        public ProductDto Product { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string Q1 { get; set; }
        public SizeDto Size { get; set; }
        public ColorDto Color { get; set; }
    }
}
