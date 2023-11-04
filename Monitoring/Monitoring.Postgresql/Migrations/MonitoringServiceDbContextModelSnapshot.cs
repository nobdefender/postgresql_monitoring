﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monitoring.Posgresql.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Monitoring.Postgresql.Migrations
{
    [DbContext(typeof(MonitoringServiceDbContext))]
    partial class MonitoringServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.Access.ActionDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.Bindings.TelegramToUserToActionDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActiondId")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("TelegramBotUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActiondId");

                    b.HasIndex("TelegramBotUserId");

                    b.ToTable("TelegramBotUserToAction");
                });

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.TelegramBot.TelegramBotUserDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<long>("TelegramChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TelegramBotUsers");
                });

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.WebAuth.WebUserDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WebUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmailAddress = "admin@gmail.ru",
                            LastName = "Admin_lastname",
                            Name = "Admin",
                            Password = "oKPLwtnO+/yAnIiNhuDVDtOUwo67CERyInTV3MV66r0DJBFFcUdMnoLCoPj0LpClIHHeCCs9169KJisL6o7VfQ==",
                            Role = "Admin",
                            Username = "Admin_username"
                        });
                });

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.Bindings.TelegramToUserToActionDbModel", b =>
                {
                    b.HasOne("Monitoring.Posgresql.Infrastructure.Models.Access.ActionDbModel", "ActionDbModel")
                        .WithMany("UserToActionDbModels")
                        .HasForeignKey("ActiondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Monitoring.Posgresql.Infrastructure.Models.TelegramBot.TelegramBotUserDbModel", "TelegramBotUserDbModel")
                        .WithMany("UserToActionDbModels")
                        .HasForeignKey("TelegramBotUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActionDbModel");

                    b.Navigation("TelegramBotUserDbModel");
                });

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.Access.ActionDbModel", b =>
                {
                    b.Navigation("UserToActionDbModels");
                });

            modelBuilder.Entity("Monitoring.Posgresql.Infrastructure.Models.TelegramBot.TelegramBotUserDbModel", b =>
                {
                    b.Navigation("UserToActionDbModels");
                });
#pragma warning restore 612, 618
        }
    }
}
