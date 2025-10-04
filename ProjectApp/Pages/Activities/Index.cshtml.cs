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
    public class IndexModel : PageModel
    {
        private readonly ProjectApp.Data.ApplicationDbContext _context;

        public IndexModel(ProjectApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Activity> Activity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Activity = await _context.Activity
                .Include(a => a.Project).ToListAsync();
        }
    }
}
