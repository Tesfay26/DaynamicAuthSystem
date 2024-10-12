using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Domain.Interfaces
{
    public interface IRolePermissionService
    {
        Task AssignRolePermissionAsync(string roleId, int moduleId, int actionId);
        Task<bool> AuthorizeUserActionAsync(string userId, string moduleName, string actionName);

    }
}
