using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printingsystem.Data;
using printingsystem.Models;
using printingsystem.Models.Files;
using printingsystem.Models.Prints;
using System.Diagnostics;
using System.Security.Claims;

namespace printingsystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            this._context = context;

        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, int copies, string paper_type, string printer, int user_id)
        {
            try
            {
                foreach (var file in files)
                {
                    string fileName = file.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var model = new FilesModel();
                    var filesEntity = new Files()
                    {
                        file_id = Guid.NewGuid(),
                        fk_user_id = user_id,
                        filename = fileName,
                        copies = copies,
                        paper_type = paper_type,
                        printer = printer,
                        status = 1
                    };

                    _context.files.Add(filesEntity);
                }

                await _context.SaveChangesAsync();

                ViewBag.Message = "Files uploaded successfully.";
            }
            catch
            {
                ViewBag.Message = "Error while uploading the files.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPrints(List<IFormFile> files, int[] paper_count, int[] no_of_copies, string[] type_of_paper, string[] printer_name, string notes)
        {
            var stats = 0;
            try
            {
                var i = 0;
                foreach (var file in files)
                {
                    string fileName = file.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var filesEntity = new Prints()
                    {
                        id = Guid.NewGuid(),
                        no_of_copies = no_of_copies[i],
                        type_of_paper = type_of_paper[i],
                        printer_name = printer_name[i],
                        no_of_pages = paper_count[i],
                        files = fileName,
                        notes = notes,
                    };

                    _context.prints.Add(filesEntity);
                    i++;
                }

                await _context.SaveChangesAsync();
                stats = 1;
            }
            catch
            {
                stats = 0;
            }

            var result = new
            {
                message = (stats == 1) ? "Files uploaded successfully." : "Error while uploading the files.",
                status = (stats == 1)
            };
            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var data = await _context.prints.Where(x => x.status == 1 || x.status == 0).ToListAsync();

            return View(data);
        }
    }
}