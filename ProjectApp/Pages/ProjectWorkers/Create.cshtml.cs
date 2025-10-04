using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.ProjectWorkers
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
        public ProjectWorker ProjectWorker { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
                ViewData["WorkerId"] = new SelectList(_context.Worker, "Id", "Email");

                return Page();
            }

            _context.ProjectWorker.Add(ProjectWorker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
