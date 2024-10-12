using DynamicAuthSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DynamicAuthSystem.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
    }
}
