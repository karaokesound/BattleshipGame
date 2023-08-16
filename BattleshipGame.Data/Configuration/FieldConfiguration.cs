﻿using BattleshipGame.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleshipGame.Data.Configuration
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.X);
            builder.Property(f => f.Y);
            builder.Property(f => f.ShipSize);
            builder.Property(f => f.IsEmpty);
            builder.Property(f => f.IsHitted);
            builder.Property(f => f.IsValid);
        }
    }
}
