using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Infrastructure.Configurations
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permissions");

            builder.HasKey(rp => new {rp.RoleId, rp.PermissionId});

            builder.HasData(
                new RolePermission
                {
                    RoleId = Role.Registered.Id,
                    PermissionId = Permission.UsersRead.Id
                });
        }
    }
}
