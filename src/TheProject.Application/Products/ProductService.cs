namespace TheProject.Application.Products
{
    using Core.Repositories;
    using Core.Entities;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.ErrorHandlers;
    using DTOs;
    using System.Collections.Generic;

    public class ProductService<TContext> : IProductService<TContext>
    {
        private readonly IRepository<TContext, ProductVariation> _productVariationRepo;
        private readonly IErrorHandler _errorHandler;

        public ProductService(IRepository<TContext, ProductVariation> productVariationRepo, IErrorHandler errorHandler)
        {
            _productVariationRepo = productVariationRepo;
            _errorHandler = errorHandler;
        }

        public async Task<List<ProductVariationDto>> ListProductVariations()
        {
            var list = _productVariationRepo.GetAll().Select(p => new ProductVariationDto()
            {
                Id = p.Id,
                Color = new ColorDto() { Id = p.Color.Id, Name = p.Color.Name },
                DeliveredIn = p.DeliveredIn,
                DiscountPrice = p.DiscountPrice,
                Key = p.Key,
                Price = p.Price,
                Product = new ProductDto() { Id = p.Product.Id, ArtikelCode = p.Product.ArtikelCode, ColorCode = p.Product.ColorCode, Description = p.Product.Description },
                Q1 = p.Q1,
                Size = new SizeDto() { Id = p.Size.Id, Name = p.Size.Name }
            }).ToList();
            //I could use AutoMapper above but I also had to make mappings for them
            return list;
        }
    }

    public interface IProductService<TContext>
    {
        Task<List<ProductVariationDto>> ListProductVariations();
    }
}
