using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Abstractions.BaseEntities
{
    public class ApplicationUser : IdentityUser<uint>
    {
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
