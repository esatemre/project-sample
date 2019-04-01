namespace TheProject.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Application.Products;
    using EFCore;
    using FileStore;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController
    {
        private readonly IProductService<AppDbContext> _dbProductService;
        private readonly IProductService<AppFileContext> _fileProductService;

        public ProductController(IProductService<AppDbContext> dbProductService, IProductService<AppFileContext> fileProductService)
        {
            _dbProductService = dbProductService;
            _fileProductService = fileProductService;
        }

        [HttpGet("Variation/Database")]
        public async Task<IActionResult> GetProductVariationsFromDatabase()
        {
            var result =  await _dbProductService.ListProductVariations();
            return new OkObjectResult(result);
        }

        [HttpGet("Variation/File")]
        public async Task<IActionResult> GetProductVariationsFromFile()
        {
            var result = await _fileProductService.ListProductVariations();
            return new OkObjectResult(result);
        }
        
    }
}
