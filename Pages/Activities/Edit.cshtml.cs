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

        public User? AssignedUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity =  await _context.Activity
                .Include(a => a.AssignedTo) // Eagerly load the assigned worker
                .FirstOrDefaultAsync(m => m.Id == id);

            if (activity == null)
            {
                return NotFound();
            }
            Activity = activity;

            if (activity.AssignedTo != null)
            {
                // If a user is assigned, set the AssignedUser property for the dropdown
                AssignedUser = new User { 
                    Id = activity.AssignedTo.Id, 
                    FirstName = activity.AssignedTo.FirstName, 
                    LastName = activity.AssignedTo.LastName 
                };
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Find the original activity from the database
            var activityToUpdate = await _context.Activity.FindAsync(Activity.Id);

            if (activityToUpdate == null)
            {
                return NotFound();
            }

            // Update only the properties that can be changed from the form
            activityToUpdate.Title = Activity.Title;
            activityToUpdate.AssignedToId = Activity.AssignedToId; // This should now work

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

        public async Task<JsonResult> OnGetUsers(string term)
        {
            var users = await _context.Worker
                .Where(u => string.IsNullOrEmpty(term) || (u.FirstName + " " + u.LastName).Contains(term))
                .Select(u => new { id = u.Id, text = u.FirstName + " " + u.LastName })
                .ToListAsync();

            return new JsonResult(users);
        }
    }
}
