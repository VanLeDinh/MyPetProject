using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;
        private string directoryPath = "";
        private string filesStorageDirectory = "FilesStorage";

        public FileController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
            directoryPath = $"{_hostingEnvironment.ContentRootPath}\\{filesStorageDirectory}";
        }

        // Async function to handle the file upload request
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            CheckAndCreateDirectory();
            if (file.Length > 0)
            {
                string filePath = Path.Combine(directoryPath, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Ok(file);
            }

            return BadRequest();
        }

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            CheckAndCreateDirectory();
            // Check if the file exists
            var fileExist = Path.Combine(directoryPath, fileName);
            if (!System.IO.File.Exists(fileExist))
            {
                // Return a 404 Not Found response if the file does not exist
                return NotFound();
            }

            // Read the file data
            var fileBytes = System.IO.File.ReadAllBytes(fileExist);

            // Return the file as a download
            return File(fileBytes, "application/octet-stream", fileName);
        }

        private void CheckAndCreateDirectory()
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
