﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Infrastructure.Configurations
{
    internal sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");

            builder.HasKey(a => a.Id);

            builder.Property(sub => sub.Threshold).IsRequired();

            builder.Property(sub => sub.TickerSymbol).IsRequired();

            builder.Property(sub => sub.UserNotified).HasDefaultValue(false);

            builder.Property(sub => sub.Position).HasConversion<int>().IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(sub => sub.UserId);
        }
    }
}
