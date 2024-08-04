using System.Linq;
using Data.Entities.Access;
using Data.Entities.Account;
using Data.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AngularEshopDbContext : DbContext
    {
        #region constructor

        public AngularEshopDbContext(DbContextOptions<AngularEshopDbContext> options) : base(options)
        {
        }

        #endregion


        #region Db Sets

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }


        #endregion

        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}