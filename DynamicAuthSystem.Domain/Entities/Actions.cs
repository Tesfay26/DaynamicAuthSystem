using System.ComponentModel.DataAnnotations;

namespace DynamicAuthSystem.Domain.Entities
{
    public class Actions
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
