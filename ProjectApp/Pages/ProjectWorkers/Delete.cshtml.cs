using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.ProjectWorkers
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public DeleteModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProjectWorker ProjectWorker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectworker = await _context.ProjectWorker.FirstOrDefaultAsync(m => m.Id == id);

            if (projectworker is not null)
            {
                ProjectWorker = projectworker;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectworker = await _context.ProjectWorker.FindAsync(id);
            if (projectworker != null)
            {
                ProjectWorker = projectworker;
                _context.ProjectWorker.Remove(ProjectWorker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
