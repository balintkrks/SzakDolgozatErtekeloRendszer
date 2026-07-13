using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO.Compression;
using SzakdolgozatErtekeloApi.DTO;
using SzakdolgozatErtekeloApi.Services;

namespace SzakdolgozatErtekeloApi.Controllers
{
    [Route("szakdolgozatErtekelo/[controller]")]
    [ApiController]
    public class AdatokController : ControllerBase
    {
        //private readonly IWebHostEnvironment _env;
        private readonly PdfService _pdfService;
        private readonly WordService _wordService;
        private readonly LatexService _latexService;
        string folder = string.Empty;

        public AdatokController(IWebHostEnvironment env, PdfService pdfService, WordService wordService, LatexService latexService)
        {
            //_env = env;
            _pdfService = pdfService;
            _wordService = wordService;
            _latexService = latexService;
            folder = Path.Combine(env.ContentRootPath, "Doksik");
        }

        //Letezik-e a mappa ha nem megcsinálni
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
            try
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
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        //Word generalasa
        [HttpPost("get-word")]
        public IActionResult GetWord([FromBody] AdatokDTO adatok)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        //LaTex generalasa
        [HttpPost("get-latex")]
        public IActionResult GetLaTex([FromBody] AdatokDTO adatok)
        {
            try
            {
                KonyvtarEllenorzes();

                string path = Path.Combine(folder, "Biralat.tex");

                _latexService.Generate(path, adatok);

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
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        //Zip generalasa
        [HttpPost("get-zip")]
        public IActionResult GetZip([FromBody] AdatokDTO adatok)
        {
            try
            {
                GetPdf(adatok);
                GetLaTex(adatok);
                GetWord(adatok);

                List<string> fileokLista = Directory
                    .GetFiles("Doksik")
                    .ToList();

                MemoryStream memoryStream = new MemoryStream();

                ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);
                foreach (string file in fileokLista)
                {
                    zipArchive.CreateEntryFromFile(file, Path.GetFileName(file));
                }

                zipArchive.Dispose();

                memoryStream.Position = 0;

                return File(memoryStream.ToArray(), "application/zip", "Dokumentumok.zip");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
