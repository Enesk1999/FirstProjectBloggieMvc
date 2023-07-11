using BloggieMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloggieMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageInterface _imageInterface;
        public ImageController(IImageInterface imageInterface)
        {
            _imageInterface = imageInterface;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // repositories called
            var imageURL = await _imageInterface.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Something Went Wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageURL });
        }
    }
}
