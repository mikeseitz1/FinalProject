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

namespace ProjectApp.Pages.Activities
{
    public class EditModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public EditModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity =  await _context.Activity.FirstOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }
            Activity = activity;
           ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
            ViewData["AssignedToId"] = new SelectList(_context.Worker, "Id", "FullName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
                ViewData["AssignedToId"] = new SelectList(_context.Worker, "Id", "FullName");

                return Page();
            }

            _context.Attach(Activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(Activity.Id))
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

        private bool ActivityExists(int id)
        {
            return _context.Activity.Any(e => e.Id == id);
        }
    }
}
