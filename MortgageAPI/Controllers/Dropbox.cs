using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO;
using System.Threading.Tasks;
using Service.Services;
namespace MortgageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DropboxController : ControllerBase
    {
        private readonly DropboxService _dropboxService;
        public DropboxController()
        {
            this._dropboxService = new DropboxService();
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
        //[HttpGet("download/{fileName}")]
        //public async Task<IActionResult> DownloadFile(string fileName)
        //{
        //    try
        //    {
        //        var fileBytes = await _dropboxService.DownloadFileFromDropbox(fileName);

        //        // Return file content as a download
        //        return File(fileBytes, "application/octet-stream", fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading file: {ex.Message}");
        //    }
        //}

    }

}
