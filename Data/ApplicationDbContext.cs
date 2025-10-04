using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Models;

namespace ProjectApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProjectApp.Models.Comment> Comment { get; set; } = default!;
        public DbSet<ProjectApp.Models.Worker> Worker { get; set; } = default!;
        public DbSet<ProjectApp.Models.ProjectWorker> ProjectWorker { get; set; } = default!;
        public DbSet<ProjectApp.Models.Project> Project { get; set; } = default!;
        public DbSet<ProjectApp.Models.Activity> Activity { get; set; } = default!;
    }
}
