namespace TheProject.Application.FileImports
{
    using Core.Repositories;
    using Core.Entities;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.ErrorHandlers;
    using Core;
    using DTOs;

    public class FileImportService<TContext> : IFileImportService<TContext>
    {
        private readonly IRepository<TContext, Product> _productRepo;
        private readonly IRepository<TContext, ProductVariation> _productVariationRepo;
        private readonly IRepository<TContext, Color> _colorRepo;
        private readonly IRepository<TContext, Size> _sizeRepo;
        private readonly IErrorHandler _errorHandler;

        public FileImportService(IRepository<TContext, Product> productRepo, IRepository<TContext, ProductVariation> productVariationRepo, IRepository<TContext, Color> colorRepo, IRepository<TContext, Size> sizeRepo, IErrorHandler errorHandler)
        {
            _productRepo = productRepo;
            _productVariationRepo = productVariationRepo;
            _colorRepo = colorRepo;
            _sizeRepo = sizeRepo;
            _errorHandler = errorHandler;
        }

        public async Task<Response> ImportFile(FileImportDto dto)
        {
            var response = new Response();
            var lines = System.IO.File.ReadLines(dto.UploadedFileName);
            foreach (var line in lines.Skip(1)) //Skip Header
            {
                try
                {
                    var splittedRow = line.Split(',');
                    if (splittedRow.Length != 10)
                        response.Warnings.Add(string.Format(_errorHandler.GetMessage(ErrorMessages.InputRowColumnCountIsNotExpected), line));
                    else
                    {
                        decimal price = 0, discount = 0;
                        if (!decimal.TryParse(splittedRow[4], out price))
                            response.Warnings.Add(string.Format(_errorHandler.GetMessage(ErrorMessages.WrongFormat), "Price", line));
                        if (!decimal.TryParse(splittedRow[5], out discount))
                            response.Warnings.Add(string.Format(_errorHandler.GetMessage(ErrorMessages.WrongFormat), "DiscountPrice", line));

                        //Product
                        var product = _productRepo.GetAll().FirstOrDefault(p => p.ArtikelCode == splittedRow[1] && p.ColorCode == splittedRow[2] && p.Description == splittedRow[3]);
                        if (product == null)
                            product = await _productRepo.Add(new Product() { ArtikelCode = splittedRow[1], ColorCode = splittedRow[2], Description = splittedRow[3] });
                        //Color
                        var color = _colorRepo.GetAll().FirstOrDefault(p => p.Name == splittedRow[9]);
                        if (color == null)
                            color = await _colorRepo.Add(new Color() { Name = splittedRow[9] });
                        //Size
                        var size = _sizeRepo.GetAll().FirstOrDefault(p => p.Name == splittedRow[8]);
                        if (size == null)
                            size = await _sizeRepo.Add(new Size() { Name = splittedRow[8] });

                        var productVar = new ProductVariation()
                        {
                            Key = splittedRow[0],
                            Price = price,
                            DiscountPrice = discount,
                            DeliveredIn = splittedRow[6],
                            Q1 = splittedRow[7],
                            ColorId = color.Id,
                            ProductId = product.Id,
                            SizeId = size.Id,
                        };

                        await _productVariationRepo.Add(productVar);
                    }
                }
                catch (System.Exception ex)
                {
                    response.HasError = true;
                    response.Errors.Add(string.Format(_errorHandler.GetMessage(ErrorMessages.UnexpectedError), ex.Message, line));
                }
            }

            return response;
        }
    }

    public interface IFileImportService<TContext>
    {
        Task<Response> ImportFile(FileImportDto dto);

    }
}
