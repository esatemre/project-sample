namespace TheProject.Application.Products.DTOs
{
    using Core.DTOs;

    public class ProductDto : BaseDto<int>
    {
        public string ArtikelCode { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
    }
}
