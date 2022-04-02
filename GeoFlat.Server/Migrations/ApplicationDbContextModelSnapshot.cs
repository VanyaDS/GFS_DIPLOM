﻿// <auto-generated />
using System;
using GeoFlat.Server.Models.Database.Entities.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeoFlat.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("password");

                    b.Property<int?>("RoleId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "Email_Index")
                        .IsUnique();

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "geoflatbel@gmail.com",
                            Password = "Password1",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Comparison", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RecordId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.HasIndex("UserId");

                    b.ToTable("Comparison");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RecordId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Flat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Area")
                        .HasColumnType("float")
                        .HasColumnName("area");

                    b.Property<int>("Floor")
                        .HasColumnType("int")
                        .HasColumnName("floor");

                    b.Property<int>("GeolocationId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int")
                        .HasColumnName("room_number");

                    b.HasKey("Id");

                    b.HasIndex("GeolocationId");

                    b.ToTable("Flat");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Geolocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("city_name");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("house_number");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("street_name");

                    b.HasKey("Id");

                    b.ToTable("Geolocation");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("message_text");

                    b.Property<int?>("Recipient")
                        .HasColumnType("int")
                        .HasColumnName("recipient");

                    b.Property<int?>("Sender")
                        .HasColumnType("int")
                        .HasColumnName("sender");

                    b.HasKey("Id");

                    b.HasIndex("Recipient");

                    b.HasIndex("Sender");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<bool>("ForDay")
                        .HasColumnType("bit")
                        .HasColumnName("for_day");

                    b.Property<bool>("HasFurniture")
                        .HasColumnType("bit")
                        .HasColumnName("has_furniture");

                    b.Property<bool>("IsAgent")
                        .HasColumnType("bit")
                        .HasColumnName("is_agent");

                    b.Property<bool>("NotForStudents")
                        .HasColumnType("bit")
                        .HasColumnName("not_for_students");

                    b.Property<string>("PicturesPath")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("pictures_path");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("publication_date");

                    b.Property<string>("RecordTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("record_title");

                    b.Property<bool>("RentStatus")
                        .HasColumnType("bit")
                        .HasColumnName("rent_status");

                    b.Property<bool>("RentType")
                        .HasColumnType("bit")
                        .HasColumnName("rent_type");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("WithInternet")
                        .HasColumnType("bit")
                        .HasColumnName("with_internet");

                    b.Property<bool>("WithoutAnimals")
                        .HasColumnType("bit")
                        .HasColumnName("without_animals");

                    b.Property<bool>("WithoutChildren")
                        .HasColumnType("bit")
                        .HasColumnName("without_children");

                    b.HasKey("Id");

                    b.HasIndex("FlatId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int")
                        .HasColumnName("access_level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessLevel = 0,
                            Name = "administrator"
                        },
                        new
                        {
                            Id = 2,
                            AccessLevel = 1,
                            Name = "moderator"
                        },
                        new
                        {
                            Id = 3,
                            AccessLevel = 2,
                            Name = "client"
                        });
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            Name = "admin",
                            PhoneNumber = "+375291110011",
                            Surname = "admin"
                        });
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Account", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.Role", "Role")
                        .WithOne("Account")
                        .HasForeignKey("GeoFlat.Server.Models.Database.Entities.Account", "RoleId")
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Comparison", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.Record", "Record")
                        .WithMany("Comparisons")
                        .HasForeignKey("RecordId");

                    b.HasOne("GeoFlat.Server.Models.Database.Entities.User", "User")
                        .WithMany("Comparisons")
                        .HasForeignKey("UserId");

                    b.Navigation("Record");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Favorite", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.Record", "Record")
                        .WithMany("Favorites")
                        .HasForeignKey("RecordId");

                    b.HasOne("GeoFlat.Server.Models.Database.Entities.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId");

                    b.Navigation("Record");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Flat", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.Geolocation", "Geolocation")
                        .WithMany("Flats")
                        .HasForeignKey("GeolocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Geolocation");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Message", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.User", "UserRecipient")
                        .WithMany("ReceviedMessages")
                        .HasForeignKey("Recipient");

                    b.HasOne("GeoFlat.Server.Models.Database.Entities.User", "UserSender")
                        .WithMany("SentMessages")
                        .HasForeignKey("Sender");

                    b.Navigation("UserRecipient");

                    b.Navigation("UserSender");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Record", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.Flat", "Flat")
                        .WithOne("Record")
                        .HasForeignKey("GeoFlat.Server.Models.Database.Entities.Record", "FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeoFlat.Server.Models.Database.Entities.User", "User")
                        .WithMany("Records")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.User", b =>
                {
                    b.HasOne("GeoFlat.Server.Models.Database.Entities.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("GeoFlat.Server.Models.Database.Entities.User", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Account", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Flat", b =>
                {
                    b.Navigation("Record");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Geolocation", b =>
                {
                    b.Navigation("Flats");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Record", b =>
                {
                    b.Navigation("Comparisons");

                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.Role", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });

            modelBuilder.Entity("GeoFlat.Server.Models.Database.Entities.User", b =>
                {
                    b.Navigation("Comparisons");

                    b.Navigation("Favorites");

                    b.Navigation("ReceviedMessages");

                    b.Navigation("Records");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
