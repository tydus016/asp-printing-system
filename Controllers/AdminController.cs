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
            var data = await _context.prints
                .Where(x => x.status == 1 || x.status == 0)
                .ToListAsync();

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

        public async Task<IActionResult> Papers()
        {
            //var data = await _context.papers.ToListAsync();

            var data = await _context.papers
                .GroupBy(y => y.date) // Replace 'ColumnName' with the actual column name
                .Select(g => new
                {
                    date = g.Key,
                    Count = g.Count(),
                    // Other properties you want to select
                })
            .OrderByDescending(g => g.date)
            .ToListAsync();

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewPaper(string id)
        {
            //var data = await _context.papers.ToListAsync();

            var data = await _context.papers
                .Where(x => x.date == id)
                .GroupBy(y => y.paper_name) // Replace 'ColumnName' with the actual column name
                .Select(g => new
                {
                    paper_name = g.Key,
                    Count = g.Count(),
                    // Other properties you want to select
                })
            .OrderByDescending(g => g.paper_name)
            .ToListAsync();

            return Json(data);
        }

        public IActionResult Paper()
        {
            return View();
        }

    }
}
