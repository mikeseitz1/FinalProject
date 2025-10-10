using Microsoft.AspNetCore.Identity;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Services
{
    public class SignInTrackingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SignInTrackingService> _logger;

        public SignInTrackingService(ApplicationDbContext context, ILogger<SignInTrackingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task LogSignInAsync(string email, string? ipAddress = null, string? userAgent = null)
        {
            try
            {
                var signInLog = new SignInLog
                {
                    Email = email,
                    SignInTime = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    UserAgent = userAgent
                };

                _context.SignInLog.Add(signInLog);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Sign-in logged for user: {Email} at {Time}", email, signInLog.SignInTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging sign-in for user: {Email}", email);
            }
        }

        public async Task<Dictionary<string, int>> GetSignInCountsByEmailAsync()
        {
            return await Task.FromResult(
                _context.SignInLog
                    .GroupBy(log => log.Email)
                    .ToDictionary(g => g.Key, g => g.Count())
            );
        }

        public async Task<List<SignInLog>> GetRecentSignInsAsync(int count = 50)
        {
            return await Task.FromResult(
                _context.SignInLog
                    .OrderByDescending(log => log.SignInTime)
                    .Take(count)
                    .ToList()
            );
        }
    }
}