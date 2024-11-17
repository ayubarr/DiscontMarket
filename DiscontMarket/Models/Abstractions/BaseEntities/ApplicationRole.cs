using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Abstractions.BaseEntities
{
    public abstract class ApplicationRole : IdentityRole<uint>
    {
    }
}
