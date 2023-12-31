using Microsoft.AspNetCore.Identity;
using SellSavvy.Domain.Entities;
using SellSavvy.Domain.Enum;

namespace SellSavvy.Domain.Identity
{
    public class Person : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public Gender Gender { get; set; }
        public List<ProductPerson> ProductPersons { get; set; } = new List<ProductPerson>();
    }
}
