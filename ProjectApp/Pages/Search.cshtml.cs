using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private const int DefaultPageSize = 10;

        public SearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get; set; } = new List<Project>();

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Search")]
        [StringLength(100, ErrorMessage = "Search term cannot be longer than 100 characters.")]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchField { get; set; } = "All";

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = DefaultPageSize;

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // Search field options for dropdown
        public List<(string Value, string Text)> SearchFieldOptions { get; } = new()
        {
            ("All", "All Fields"),
            ("Name", "Project Name"),
            ("Description", "Description"),
            ("PM", "Project Manager"),
            ("Status", "Status")
        };

        // Status options for filtering
        public List<string> StatusOptions { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            // Load status options for dropdown
            await LoadStatusOptionsAsync();

            var query = BuildSearchQuery();

            // Get total count for pagination
            TotalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);

            // Apply pagination
            Projects = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .Include(p => p.PM)
                .AsNoTracking()
                .ToListAsync();
        }

        private IQueryable<Project> BuildSearchQuery()
        {
            var query = _context.Project.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                var searchTerm = SearchString.Trim();
                
                query = SearchField?.ToLower() switch
                {
                    "name" => query.Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%")),
                    "description" => query.Where(p => EF.Functions.Like(p.Description, $"%{searchTerm}%")),
                    "pm" => query.Where(p => p.PM != null && 
                        (EF.Functions.Like(p.PM.FirstName, $"%{searchTerm}%") ||
                         EF.Functions.Like(p.PM.LastName, $"%{searchTerm}%") ||
                         EF.Functions.Like(p.PM.Email, $"%{searchTerm}%"))),
                    "status" => query.Where(p => EF.Functions.Like(p.Status, $"%{searchTerm}%")),
                    _ => query.Where(p => 
                        EF.Functions.Like(p.Name, $"%{searchTerm}%") ||
                        EF.Functions.Like(p.Description, $"%{searchTerm}%") ||
                        EF.Functions.Like(p.Status, $"%{searchTerm}%") ||
                        (p.PM != null && 
                            (EF.Functions.Like(p.PM.FirstName, $"%{searchTerm}%") ||
                             EF.Functions.Like(p.PM.LastName, $"%{searchTerm}%") ||
                             EF.Functions.Like(p.PM.Email, $"%{searchTerm}%"))))
                };
            }

            // Apply status filter
            if (!string.IsNullOrWhiteSpace(Status) && Status != "All")
            {
                query = query.Where(p => p.Status == Status);
            }

            // Default ordering
            query = query.OrderBy(p => p.Name);

            return query;
        }

        private async Task LoadStatusOptionsAsync()
        {
            var statuses = await _context.Project
                .Select(p => p.Status)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            StatusOptions = new List<string> { "All" };
            StatusOptions.AddRange(statuses);
        }

        public string GetPageUrl(int pageNumber)
        {
            var queryParams = new List<string>();
            
            if (!string.IsNullOrWhiteSpace(SearchString))
                queryParams.Add($"SearchString={Uri.EscapeDataString(SearchString)}");
            
            if (!string.IsNullOrWhiteSpace(SearchField) && SearchField != "All")
                queryParams.Add($"SearchField={SearchField}");
            
            if (!string.IsNullOrWhiteSpace(Status) && Status != "All")
                queryParams.Add($"Status={Status}");
            
            if (PageSize != DefaultPageSize)
                queryParams.Add($"PageSize={PageSize}");
            
            queryParams.Add($"PageNumber={pageNumber}");

            return $"?{string.Join("&", queryParams)}";
        }
    }
}