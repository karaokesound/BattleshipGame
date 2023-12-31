﻿using BattleshipGame.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleshipGame.Data.DbContexts
{
    public class BattleshipGameDbContext : DbContext
    {
        public DbSet<PlayerEntity> Players { get; set; } = null!;

        public DbSet<FieldEntity> Fields { get; set; } = null!;

        public DbSet<GameEntity> Games { get; set; } = null!;

        public BattleshipGameDbContext(DbContextOptions<BattleshipGameDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerEntity>().HasData(
                new PlayerEntity("Player1", "City1")
                {
                    Id = 1,
                    CanShoot = true
                },
                new PlayerEntity("Player2", "City1")
                {
                    Id = 2,
                    CanShoot = true
                },
                new PlayerEntity("Player3", "City1")
                {
                    Id = 3,
                    CanShoot = true
                },
                new PlayerEntity("Player4", "City1")
                {
                    Id = 4,
                    CanShoot = true
                },
                new PlayerEntity("Player5", "City1")
                {
                    Id = 5,
                    CanShoot = true
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
