using Microsoft.AspNetCore.Identity;
using ProjectApp.Services;

namespace ProjectApp.Services
{
    public class SignInEventHandler
    {
        private readonly SignInTrackingService _signInTrackingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignInEventHandler(SignInTrackingService signInTrackingService, IHttpContextAccessor httpContextAccessor)
        {
            _signInTrackingService = signInTrackingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task HandleSignInAsync(string email)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString();
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();

            await _signInTrackingService.LogSignInAsync(email, ipAddress, userAgent);
        }
    }
}