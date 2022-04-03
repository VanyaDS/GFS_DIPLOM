
using GeoFlat.Server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GeoFlat.Server.Models.Database.Entities.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //   Database.EnsureCreated();   
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Geolocation> Geolocations { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>().HasOne(x => x.UserSender)
                                     .WithMany(x => x.SentMessages)
                                     .HasForeignKey(x => x.Sender)
                                     .OnDelete(DeleteBehavior.ClientSetNull);


            builder.Entity<Message>().HasOne(x => x.UserRecipient)
                                     .WithMany(x => x.ReceviedMessages)
                                     .HasForeignKey(x => x.Recipient)
                                     .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Favorite>().HasOne(x => x.User)
                                     .WithMany(x => x.Favorites)
                                     .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Favorite>().HasOne(x => x.Record)
                                     .WithMany(x => x.Favorites)
                                     .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Comparison>().HasOne(x => x.User)
                                     .WithMany(x => x.Comparisons)
                                     .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Comparison>().HasOne(x => x.Record)
                                     .WithMany(x => x.Comparisons)
                                     .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Account>().HasData(
            new Account
            {
                Id = 1,
                Email = "geoflatbel@gmail.com",
                Password = HashingMD5.GetHashStringMD5("Password1"),
                Role = RoleHealper.ADMIN
            });

            builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "admin",
                Surname = "admin",
                PhoneNumber = "+375291110011",
                AccountId = 1
            });
        }
    }
}