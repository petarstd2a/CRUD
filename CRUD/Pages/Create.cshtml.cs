﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public Employee Employee { get; set; }

        [TempData]
        public string Message { get; set; }

        private ILogger<CreateModel> _log;

        public CreateModel(AppDbContext db,
            ILogger<CreateModel> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _db.Employees.Add(Employee);
            
            await _db.SaveChangesAsync();

            var msg = $"Employee {Employee.Name} added.";
            Message = msg;
            _log.LogCritical(msg);

            return RedirectToPage("/Index");
        }
    }
}