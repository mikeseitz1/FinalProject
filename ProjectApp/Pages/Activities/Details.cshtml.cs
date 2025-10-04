using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.Activities
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public DetailsModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Activity Activity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activity.FirstOrDefaultAsync(m => m.Id == id);

            if (activity is not null)
            {
                Activity = activity;

                return Page();
            }

            return NotFound();
        }
    }
}
