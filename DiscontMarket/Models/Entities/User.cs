using DiscontMarket.Domain.Models.Abstractions.BaseEntities;

namespace DiscontMarket.Domain.Models.Entities
{
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string? SupportContacts { get; set; }
        
        public string? ClientsVk { get; set; }
        
        public string? ClientsTelegram { get; set; }
        
        public string? ClientsWhatsapp { get; set; }
        
        public string? WorkTimeInfo { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string? ReturnsText  { get; set; }
        
        public string? TextAdress { get; set; }
        
        public string? HrefAdress { get; set; }
        
        public string? HrefmapAdress { get; set; }

        public string? ContactInfoText { get; set; }
        
        public string? Email { get; set; }
        
        public List<Product>? Products { get; set; }
    }
}
