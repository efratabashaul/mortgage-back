using Microsoft.AspNetCore.Mvc;
using Service.Services;
namespace MortgageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DropboxController : ControllerBase
    {
        private readonly DropboxService _dropboxService;

        public DropboxController(DropboxService dropboxService)
        {
            _dropboxService = dropboxService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            Console.WriteLine("in upload post!");
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                Console.WriteLine("in upload post! in try");

                var fileMetadata = await _dropboxService.UploadFileToDropbox(file);
                return Ok(new { FileName = fileMetadata.Name, FilePath = fileMetadata.PathDisplay });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }       
        [HttpPost("uploadfiles")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            Console.WriteLine("in upload post!");
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            try
            {
                Console.WriteLine("in upload post! in try");

                var uploadedFilesMetadata = await _dropboxService.UploadFilesToDropbox(files);

                var result = uploadedFilesMetadata.Select(fileMetadata => new
                {
                    FileName = fileMetadata.Name,
                    FilePath = fileMetadata.PathDisplay
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading files: {ex.Message}");
            }
        } 
    }
}
