using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Entities
{
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public List<Product>? Products { get; set; }
    }
}
