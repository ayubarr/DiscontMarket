using Microsoft.AspNetCore.Identity;

namespace DiscontMarket.Domain.Models.Abstractions.BaseEntities
{
    public abstract class ApplicationUser : IdentityUser<uint>
    {
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
