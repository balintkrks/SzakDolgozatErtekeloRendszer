using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SzakdolgozatErtekeloApi.DTO;
using SzakdolgozatErtekeloApi.Services;

namespace SzakdolgozatErtekeloApi.Controllers
{
    [Route("szakdolgozatErtekelo/[controller]")]
    [ApiController]
    public class AdatokController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly PdfService _pdfService;

        public AdatokController(IWebHostEnvironment env, PdfService pdfService)
        {
            _env = env;
            _pdfService = pdfService;
            
        }

        //Pdf generalasa
        [HttpPost("get-pdf")]
        public async Task<IActionResult> GetPdf([FromBody] AdatokDTO adatok)
        {

            string folder = Path.Combine(_env.ContentRootPath, "Doksik");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }


            string path = Path.Combine(folder, "Biralat.pdf");

            _pdfService.Generate(path, adatok);

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }                

            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(path, "application/pdf","Biralat.pdf");
        }
    }
}
