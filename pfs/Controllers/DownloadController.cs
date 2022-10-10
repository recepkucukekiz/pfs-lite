using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using pfs.Models;
using System.IO;

namespace pfs.Controllers
{
    public class DownloadController : Controller
    {
        public IActionResult Index()
        {
            AuthenticationResult auth = new AuthenticationResult();
            return View(auth);
        }

        [HttpPost]
        public IActionResult Index(AuthenticationResult auth)
        {
            if (auth.Password.Equals("184800"))
            {
                List<FileModel> files = new List<FileModel>();

                try
                {
                    //Fetch all files in the Folder (Directory).
                    string[] filePaths = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "UploadedFiles/"));

                    //Copy File names to Model collection.
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
                    }
                }
                catch (Exception ex)
                {
                    auth.Files = files;
                    return View(auth);
                }

                auth.Files = files;
                return View(auth);
            }
            else
            {
                return View(auth);
            }
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(Environment.CurrentDirectory, "UploadedFiles/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        public IActionResult DeleteFile(string fileName)
        {
            try
            {
                //Build the File Path.
                string path = Path.Combine(Environment.CurrentDirectory, "UploadedFiles/") + fileName;

                System.IO.File.Delete(path);
                return RedirectToAction("List", "Download");
            }
            catch (Exception ex)
            {

                return RedirectToAction("List", "Download"); ;
            }
        }
    }
}
