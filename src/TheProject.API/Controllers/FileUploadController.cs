namespace TheProject.API.Controllers
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Application.FileImports;
    using System.Threading.Tasks;
    using EFCore;
    using FileStore;
    using Application.FileImports.DTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileImportService<AppDbContext> _dbFileImportService;
        private readonly IFileImportService<AppFileContext> _fileFileImportService;
        private readonly IHostingEnvironment _env;

        public FileUploadController(IFileImportService<AppDbContext> dbFileImportService, IFileImportService<AppFileContext> fileFileImportService, IHostingEnvironment env)
        {
            _dbFileImportService = dbFileImportService;
            _fileFileImportService = fileFileImportService;
            _env = env;
        }

        [HttpPost("UploadToDatabase")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)] //MAX 1 GB
        [RequestSizeLimit(1073741824)]
        public async Task<IActionResult> UploadToDatabase(IFormFile file)
        {
            try
            {
                var fileName = Upload(file);

                var response = await _dbFileImportService.ImportFile(new FileImportDto() { UploadedFileName = fileName }); //Parsing file into the database

                System.IO.File.Delete(fileName); //Remove file from filesystem after import

                return Ok(new { FileSize = file.Length, Result = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UploadToFile")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)] //MAX 1 GB
        [RequestSizeLimit(1073741824)]
        public async Task<IActionResult> UploadToFile(IFormFile file)
        {
            try
            {
                var fileName = Upload(file);

                var response = await _fileFileImportService.ImportFile(new FileImportDto() { UploadedFileName = fileName }); //Parsing file into the database

                System.IO.File.Delete(fileName); //Remove file from filesystem after import

                return Ok(new { FileSize = file.Length, Result = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string Upload(IFormFile file)
        {
            string fileName = $"{_env.WebRootPath}\\{file.FileName}";
            using (FileStream fs = System.IO.File.Create(fileName)) //save file to filesystem
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return fileName;
        }
    }
}
