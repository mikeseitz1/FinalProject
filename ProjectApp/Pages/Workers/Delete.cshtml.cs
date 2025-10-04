using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.Workers
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public DeleteModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Worker Worker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker.FirstOrDefaultAsync(m => m.Id == id);

            if (worker is not null)
            {
                Worker = worker;

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

            var worker = await _context.Worker.FindAsync(id);
            if (worker != null)
            {
                Worker = worker;
                _context.Worker.Remove(Worker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
