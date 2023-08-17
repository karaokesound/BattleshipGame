﻿// <auto-generated />
using BattleshipGame.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BattleshipGame.Data.Migrations
{
    [DbContext(typeof(BattleshipGameDbContext))]
    [Migration("20230817125851_seedingusers")]
    partial class seedingusers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("BattleshipGame.Data.Entities.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Player")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ShipSize")
                        .HasColumnType("INTEGER");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("BattleshipGame.Data.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "City1",
                            Name = "Player1"
                        },
                        new
                        {
                            Id = 2,
                            City = "City1",
                            Name = "Player2"
                        },
                        new
                        {
                            Id = 3,
                            City = "City1",
                            Name = "Player3"
                        },
                        new
                        {
                            Id = 4,
                            City = "City1",
                            Name = "Player4"
                        },
                        new
                        {
                            Id = 5,
                            City = "City1",
                            Name = "Player5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}