using DiscontMarket.DAL.SqlServer.Helpers;
using DiscontMarket.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscontMarket.DAL.SqlServer.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            PropertyHelper<User>.SetProperties(builder, false,
               e => e.UserName,
               e => e.NormalizedUserName,
               e => e.FirstName,
               e => e.LastName,
               e => e.MiddleName,
               e => e.Email,
               e => e.NormalizedEmail,
               e => e.PasswordHash,
               e => e.SecurityStamp,
               e => e.ConcurrencyStamp);
        }
    }    
}
