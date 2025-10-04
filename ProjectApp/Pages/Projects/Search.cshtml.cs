using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Pages.Projects
{
    public class SearchModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public SearchModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IList<Project> Project { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var projects = _context.Project.Include(p => p.PM).AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                projects = projects.Where(p => p.Name.Contains(SearchString));
            }

            Project = await projects.ToListAsync();
        }
    }
}
