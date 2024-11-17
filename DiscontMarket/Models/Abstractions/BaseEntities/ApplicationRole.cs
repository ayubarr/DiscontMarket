using Microsoft.AspNetCore.Identity;

namespace DiscontMarket.Domain.Models.Abstractions.BaseEntities
{
    public abstract class ApplicationRole : IdentityRole<uint>
    {
    }
}
