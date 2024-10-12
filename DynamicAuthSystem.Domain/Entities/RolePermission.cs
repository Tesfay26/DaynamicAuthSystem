using System.ComponentModel.DataAnnotations;

namespace DynamicAuthSystem.Domain.Entities
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int ModuleId { get; set; }
        public int ActionId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual Module Module { get; set; }
        public virtual Actions Action { get; set; }
    }
}
