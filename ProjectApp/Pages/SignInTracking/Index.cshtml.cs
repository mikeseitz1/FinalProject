using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectApp.Models;
using ProjectApp.Services;

namespace ProjectApp.Pages.SignInTracking
{
    public class IndexModel : PageModel
    {
        private readonly SignInTrackingService _signInTrackingService;

        public IndexModel(SignInTrackingService signInTrackingService)
        {
            _signInTrackingService = signInTrackingService;
        }

        public Dictionary<string, int> SignInCounts { get; set; } = new();
        public List<SignInLog> RecentSignIns { get; set; } = new();
        public int TotalSignIns { get; set; }
        public int UniqueUsers { get; set; }
        public int TodaySignIns { get; set; }
        public int WeekSignIns { get; set; }

        public async Task OnGetAsync()
        {
            SignInCounts = await _signInTrackingService.GetSignInCountsByEmailAsync();
            RecentSignIns = await _signInTrackingService.GetRecentSignInsAsync(20);
            
            TotalSignIns = SignInCounts.Values.Sum();
            UniqueUsers = SignInCounts.Count;
            
            var today = DateTime.Today;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            
            TodaySignIns = RecentSignIns.Count(log => log.SignInTime.Date == today);
            WeekSignIns = RecentSignIns.Count(log => log.SignInTime.Date >= weekStart);
        }
    }
}