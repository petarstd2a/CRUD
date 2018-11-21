using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace CRUD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IList<Employee> Employees { get; set; }

        [TempData]
        public string Message { get; set; }

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Employees = await _db.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<ActionResult> OnPostDeleteAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
