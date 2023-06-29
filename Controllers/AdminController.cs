using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using printingsystem.Data;
using System.Data;

namespace printingsystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly Context _context;

        public AdminController(Context context)
        {
            this._context = context;

        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.prints.Where(x => x.status == 1 || x.status == 0).ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Print()
        {
            var data = await _context.prints.Where(x => x.status == 1 || x.status == 0).ToListAsync();

            return View(data);
        }

        public IActionResult UpdateStatus(string id, int status)
        {
            var rowToUpdate = _context.files.Find(id);
            if (rowToUpdate != null)
            {
                rowToUpdate.status = status == 1 ? 2 : 1;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult UpdateTable(string fileName, int status)
        {
            // Retrieve the record from the table
            var record = _context.files.SingleOrDefault(b => b.file_id == Guid.Parse(fileName));

            if (record != null)
            {
                // Update the properties of the record
                record.status = 2;

                // Save the changes to the database
                _context.SaveChanges();
            }

            // Redirect to another action or return a view
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Reports()
        {
            var data = await _context.prints.Where(x => x.status == 1 || x.status == 0 || x.status == 2).ToListAsync();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateRow(Guid id, int status)
        {
            // Retrieve the existing row from the database
            var existingRow = _context.prints.Find(id);

            if (existingRow != null)
            {
                // Update the properties of the existing row
                existingRow.status = status;

                // Save the changes to the database
                _context.SaveChanges();
            }

            // If the existing row is not found, return an error response
            return Json(new { status = true, message = "Updated successfully" });
        }
    }
}
