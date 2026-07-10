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
        private readonly WordService _wordService;
        string folder = string.Empty;

        public AdatokController(IWebHostEnvironment env, PdfService pdfService, WordService wordService)
        {
            _env = env;
            _pdfService = pdfService;
            _wordService = wordService;
            folder = Path.Combine(env.ContentRootPath, "Doksik");
        }

        private void KonyvtarEllenorzes()
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        //Pdf generalasa
        [HttpPost("get-pdf")]
        public IActionResult GetPdf([FromBody] AdatokDTO adatok)
        {
            KonyvtarEllenorzes();

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

            return PhysicalFile(path, "application/pdf", "Biralat.pdf");
        }

        //Word generalasa
        [HttpPost("get-word")]
        public IActionResult GetWord([FromBody] AdatokDTO adatok)
        {
            KonyvtarEllenorzes();

            string path = Path.Combine(folder, "Biralat.docx");

            _wordService.Generate(path, adatok);

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Biralat.docx");
        }

        //LaTex generalasa
        [HttpPost("get-latex")]
        public IActionResult GetLaTex(string asd)
        {
            KonyvtarEllenorzes();

            string path = Path.Combine(_env.ContentRootPath, "Doksik", "Biralat.tex");

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(path, "tex/plain", "Biralat.tex");
        }

        //Zip generalasa
        //nincs megírva
        [HttpPost("get-zip")]
        public IActionResult GetZip(string asd)
        {

            string path = Path.Combine(_env.ContentRootPath, "Doksik", "Biralat.tex");

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(path, "tex/plain", "Biralat.tex");
        }
    }
}
