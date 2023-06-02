using DatabaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAPI
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<DBUser> Users { get; set; }
        public DbSet<DBProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<DBUser>(dbUser =>
            {
                dbUser.ToTable("users");
                dbUser.HasKey(user => user.Id);
                dbUser.Property(user => user.Id).HasColumnName("id");
                dbUser.Property(user => user.Login).HasColumnName("login");
                dbUser.Property(user => user.Password).HasColumnName("password");
                dbUser.Property(user => user.Name).HasColumnName("name");
            });

            modelBuilder.Entity<DBProduct>(dbProduct =>
            {
                dbProduct.ToTable("products");
                dbProduct.HasKey(product => product.Id);
                dbProduct.Property(product => product.Id).HasColumnName("id");
                dbProduct.Property(product => product.Name).HasColumnName("name");
                dbProduct.Property(product => product.Description).HasColumnName("description");
                dbProduct.Property(product => product.Price).HasColumnName("price");
                dbProduct.Property(product => product.IsDeleted).HasColumnName("is_deleted");
            });
        }
    }
}
