namespace MeTube.Data
{
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Common;
    using MeTube.Models;

    public class MeTubeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            builder
                .UseSqlServer(StringExtensions.ConnectionString);

            base.OnConfiguring(builder); 
        }
    }
}
