using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using DynamicAuthSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DynamicAuthSystem.Infrastructure
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly ApplicationDbContext _context;
        public RolePermissionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AssignRolePermissionAsync(string roleId, int moduleId, int actionId)
        {
            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                ModuleId = moduleId,
                ActionId = actionId
            };

            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AuthorizeUserActionAsync(string userId, string moduleName, string actionName)
        {
            //find the user's roles
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            // check if any role has permission for the given module and action
            return await _context.RolePermissions
                .Join(_context.Modules, rp => rp.ModuleId, m => m.Id, (rp,m) => new {rp, m})
                .Join(_context.Actions, rm => rm.rp.ActionId, a => a.Id, (rm, a) => new { rm.rp, rm.m, a})
                .AnyAsync(rma => userRoles.Contains(rma.rp.RoleId) && rma.m.Name == moduleName && rma.a.Name == actionName);
        }
    }
}
