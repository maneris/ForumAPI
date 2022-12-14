
using ForumAPI.Auth.Model;
using ForumAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Data
{
    public class ForumDBContext : IdentityDbContext<ForumRestUser>
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
