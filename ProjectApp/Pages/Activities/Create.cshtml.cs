using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.Activities
{
    public class CreateModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public CreateModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
            ViewData["WorkerId"] = new SelectList(_context.Worker, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Activity Activity { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Activity.Add(Activity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
