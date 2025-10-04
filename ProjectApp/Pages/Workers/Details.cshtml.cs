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
    public class DetailsModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public DetailsModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
