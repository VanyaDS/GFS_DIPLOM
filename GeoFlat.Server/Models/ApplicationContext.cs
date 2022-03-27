using Microsoft.EntityFrameworkCore;

namespace GeoFlat.Server.Models
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
        public DbSet<Role> Roles { get; set; }
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
        }
    }
}