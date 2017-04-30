using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace EFCoreGuestBook.Models{
    public class GuestBookDataContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestBook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("guestbook_id");
                entity.Property(e => e.Email).HasColumnName("guest_email");
                entity.Property(e => e.Name).HasColumnName("guest_name");
                entity.Property(e => e.Message).HasColumnName("message");
            });
        }
        public virtual DbSet<GuestBook> GuestBooks { get; set; }
    }
}
