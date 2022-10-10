using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using pfs.Interfaces;

namespace pfs.Controllers
{
    public class UploadController : Controller
    {
        readonly IBufferedFileUploadService _bufferedFileUploadService;
        readonly IStreamFileUploadService _streamFileUploadService;

        public UploadController(IBufferedFileUploadService bufferedFileUploadService, IStreamFileUploadService streamFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
            _streamFileUploadService = streamFileUploadService;
        }

        [HttpGet]
        public IActionResult BufferUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BufferUpload(IFormFile file, String password)
        {
            try
            {
                if (password != "184800")
                {
                    ViewBag.Message = "Wrong Password";
                }
                else
                {
                    if (await _bufferedFileUploadService.UploadFile(file))
                    {
                        ViewBag.Message = "File Upload Successful";
                    }
                    else
                    {
                        ViewBag.Message = "File Upload Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }

    }
}
