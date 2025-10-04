using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.ProjectWorkers
{
    public class EditModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public EditModel(ProjectApp.Data.ApplicationDbContext context)
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

            var projectworker =  await _context.ProjectWorker.FirstOrDefaultAsync(m => m.Id == id);
            if (projectworker == null)
            {
                return NotFound();
            }
            ProjectWorker = projectworker;
           ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
           ViewData["WorkerId"] = new SelectList(_context.Worker, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProjectWorker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectWorkerExists(ProjectWorker.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectWorkerExists(int id)
        {
            return _context.ProjectWorker.Any(e => e.Id == id);
        }
    }
}
