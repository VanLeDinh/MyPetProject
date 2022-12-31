using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public FileController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        // Async function to handle the file upload request
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "FileStorages");
            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Ok(file);
            }

            return BadRequest();
        }

        //[HttpGet("download/{fileId}")]
        //public async Task<IActionResult> DownloadFile(string fileId)
        //{
        //    // Get the client ID and secret from a secure configuration file or database
        //    var clientId = _configuration["Google:ClientId"];
        //    var clientSecret = _configuration["Google:ClientSecret"];
        //    // Create a new Google Drive service
        //    var driveService = new DriveService(new BaseClientService.Initializer
        //    {
        //        HttpClientInitializer = await GetCredentialsAsync(clientId, clientSecret),
        //        ApplicationName = "My App"
        //    });

        //    // Get the file metadata
        //    var file = await driveService.Files.Get(fileId).ExecuteAsync();

        //    // Check if the file is a Google Docs, Sheets, or Slides document
        //    if (file.MimeType.StartsWith("application/vnd.google-apps"))
        //    {
        //        // If the file is a Google Docs, Sheets, or Slides document, export it to the desired format
        //        var exportRequest = driveService.Files.Export(fileId, file.ExportLinks["application/pdf"]);
        //        var stream = new MemoryStream();
        //        await exportRequest.DownloadAsync(stream);

        //        // Set the content type and file name
        //        var contentType = "application/pdf";
        //        var fileName = file.Name + ".pdf";

        //        // Return the file as a download
        //        return File(stream.ToArray(), contentType, fileName);
        //    }
        //    else
        //    {
        //        // If the file is not a Google Docs, Sheets, or Slides document, download it directly
        //        var downloadRequest = driveService.Files.Get(fileId);
        //        var stream = new MemoryStream();
        //        await downloadRequest.DownloadAsync(stream);

        //        // Set the content type and file name
        //        var contentType = file.MimeType;
        //        var fileName = file.Name;

        //        // Return the file as a download
        //        return File(stream.ToArray(), contentType, fileName);
        //    }
        //}

        //private static async Task<UserCredential> GetCredentialsAsync(string clientId, string clientSecret)
        //{
        //    // Use the Client ID and Secret to request an access token
        //    var tokenResponse = await new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        //    {
        //        ClientSecrets = new ClientSecrets
        //        {
        //            ClientId = clientId,
        //            ClientSecret = clientSecret
        //        }
        //    }).ExchangeCodeForTokenAsync("", "", "", CancellationToken.None);

        //    // Use the access token to create a new Google Drive service
        //    return new UserCredential(new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        //    {
        //        ClientSecrets = new ClientSecrets
        //        {
        //            ClientId = clientId,
        //            ClientSecret = clientSecret
        //        }
        //    }), "user", tokenResponse);
        //}
    }
}
