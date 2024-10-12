using DynamicAuthSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Domain.Interfaces
{
    public interface IUserService
    {
        // Define methods that your user service should implement
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        // Add other methods as needed
    }
}
