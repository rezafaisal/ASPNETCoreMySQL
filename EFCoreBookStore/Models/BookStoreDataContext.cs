using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;

namespace EFCoreBookStore.Models{
    public class BookStoreDataContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryID).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name");
            });
            modelBuilder.Entity<Category>().HasKey(e => new { e.CategoryID});

            modelBuilder.Entity<Author>().ToTable("authors");
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.AuthorID).HasColumnName("author_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
            });
            modelBuilder.Entity<Author>().HasKey(e => new { e.AuthorID});

            modelBuilder.Entity<Book>().ToTable("books");
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.ISBN).HasColumnName("isbn");
                entity.Property(e => e.CategoryID).HasColumnName("category_id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Photo).HasColumnName("photo");
                entity.Property(e => e.PublishDate).HasColumnName("publish_date");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("qty");
            });
            modelBuilder.Entity<Book>().HasKey(e => new { e.ISBN});

            modelBuilder.Entity<BookAuthor>().ToTable("books_authors");
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.Property(e => e.ISBN).HasColumnName("isbn");
                entity.Property(e => e.AuthorID).HasColumnName("author_id");
            });
            modelBuilder.Entity<BookAuthor>().HasKey(e => new { e.ISBN, e. AuthorID });

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).HasColumnName("username");
                entity.Property(e => e.RoleID).HasColumnName("roleid");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Fullname).HasColumnName("fullname");
            });
            modelBuilder.Entity<User>().HasKey(e => new { e.UserName});

            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID).HasColumnName("roleid");
                entity.Property(e => e.RoleName).HasColumnName("rolename");
            });
            modelBuilder.Entity<Role>().HasKey(e => new { e.RoleID});
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BooksAuthors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}