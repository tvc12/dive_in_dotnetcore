using Microsoft.EntityFrameworkCore;

namespace CatBasicExample.Domain
{
    public class CatContext : DbContext
    {
        public CatContext(DbContextOptions<CatContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder moderbuilder)
        {
            base.OnModelCreating(moderbuilder);
            moderbuilder.Entity<Cat>().ToTable("Cat");
        }
    }
}