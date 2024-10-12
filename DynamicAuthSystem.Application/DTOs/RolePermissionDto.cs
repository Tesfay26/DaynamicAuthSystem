using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.DTOs
{
    public class RolePermissionDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public string ActionName { get; set; }
    }
}
