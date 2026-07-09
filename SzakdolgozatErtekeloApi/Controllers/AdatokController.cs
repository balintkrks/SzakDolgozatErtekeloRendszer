using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SzakdolgozatErtekeloApi.DTO;

namespace SzakdolgozatErtekeloApi.Controllers
{
    [Route("szakdolgozatErtekelo/[controller]")]
    [ApiController]
    public class AdatokController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public AdatokController(IWebHostEnvironment env)
        {
            _env = env;
        }

        //Pdf generalasa
        [HttpPost("get-pdf")]
        public async Task<IActionResult> GetPdf(string asd)
        {
            string path = Path.Combine(_env.ContentRootPath, "Doksik", "Biralat.pdf");

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
