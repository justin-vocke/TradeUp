using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;
using TradeUp.Infrastructure.Models;

namespace TradeUp.Infrastructure.Configurations
{
    internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(a => a.Id);

            builder.Property(user => user.Email).IsRequired();

            builder.Property(u => u.IsActive)
           .HasDefaultValue(true);

            builder.HasIndex(user => user.Email)
               .IsUnique();

            
        }
    }
}
