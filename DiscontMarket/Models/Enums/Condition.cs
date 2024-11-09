using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Enums
{
    public enum Condition
    {
        Pending = 1,
        Confirmed = 2,
        Canceled = 3,
        Failed = 4,
    }
}
