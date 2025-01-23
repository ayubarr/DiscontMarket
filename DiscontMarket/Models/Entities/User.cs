using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string? ClientsVk { get; set; }
        public string? ClientsTelegram { get; set; }
        public string? ClientsWhatsapp { get; set; }

        public List<Product>? Products { get; set; }
    }
}
