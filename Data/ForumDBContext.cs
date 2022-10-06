
using ForumAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Data
{
    public class ForumDBContext : DbContext
    {
        public ForumDBContext(DbContextOptions<ForumDBContext> options)
        : base(options)
        {
        }
        public DbSet<Topics> Topics { get;  set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Threads> Threads { get; set; }
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=ForumDb2");
        }*/
    }
}
