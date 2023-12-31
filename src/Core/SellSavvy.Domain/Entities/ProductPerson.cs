using SellSavvy.Domain.Identity;

namespace SellSavvy.Domain.Entities
{
    public class ProductPerson
    {
        public Guid PersonId { get; set; }
        public Guid ProductId { get; set; }
        public Person Person { get; set; }
        public Product Product { get; set; }

    }
}