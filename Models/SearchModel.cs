using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApp.Models
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var projects = from p in _context.Project
                         select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                projects = projects.Where(s => s.Name.Contains(SearchString) || s.Description.Contains(SearchString));
            }

            Project = await projects.Include(p => p.PM).AsNoTracking().ToListAsync();
        }
    }
}