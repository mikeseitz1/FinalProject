using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.Comments
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
        ViewData["AuthorId"] = new SelectList(_context.Worker, "Id", "Email");
        ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
