using Microsoft.EntityFrameworkCore;

namespace CatBasicExample.Domain
{
    public class CatContext : DbContext
    {
        public CatContext(DbContextOptions<CatContext> options) : base(options)
        {
        }

        public virtual DbSet<Cat> Cat { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Cat>().ToTable("Cat");
        }
    }
}