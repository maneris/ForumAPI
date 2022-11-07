using ForumAPI.Auth.Model;
using ForumAPI.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace ForumAPI.Data
{
    public class SeedData
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ForumRestUser> _userManager;
        private readonly IServiceProvider _serviceProvider;
        public SeedData(UserManager<ForumRestUser> userManager, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
        }
        public void Initialize()
        {
            

            using (var context = new ForumDBContext(
            _serviceProvider.GetRequiredService<DbContextOptions<ForumDBContext>>()))
            {
                var firstUser = _userManager.FindByNameAsync("JonasDon");
                context.Database.EnsureCreated();
                // Look for any Topics.
                if (context.Topics.Any())
                {
                    return; // DB has been seeded
                }
                context.Topics.AddRange(
                new Topics { Title="Science", Description="Topic that's focused on newest science's achievements",CreationDateTime=DateTime.Now },
                new Topics { Title = "Funny", Description = "Topic that's focused on jokes, memes and funny videos", CreationDateTime = DateTime.Now },
                new Topics { Title = "Animal", Description = "Topic that's focused on anything animal related", CreationDateTime = DateTime.Now }
                );
                context.SaveChanges();
                context.Threads.AddRange(
                new Threads { Title = "Newest nuclear reactor in France", Description="A discussion thread about the newest nuclear reactor built in France", CreationDateTime=DateTime.Now, TopicId=1,UserId = firstUser.Result.Id },
                new Threads { Title = "Mars discoveries", Description = "A discussion thread about the latest dicoveries on and about Mars", CreationDateTime = DateTime.Now, TopicId = 1, UserId = firstUser.Result.Id },
                new Threads { Title = "Animals being funny", Description = "A thread for videos of animals doing funny things", CreationDateTime = DateTime.Now, TopicId = 2, UserId = firstUser.Result.Id }
                );
                context.SaveChanges();
                context.Posts.AddRange(
               new Posts { Description = "Is it capabilities as good as the one in the Germany?",CreationDate=DateTime.Now,ThreadsId=1, UserId = firstUser.Result.Id },
               new Posts { Description = "Can it blow up from an earthquake?", CreationDate = DateTime.Now, ThreadsId = 1, UserId = firstUser.Result.Id },
               new Posts { Description = "There's been a new discovery of 10m diameter and 500m deep whole near mount Olimpus", CreationDate = DateTime.Now, ThreadsId = 2, UserId = firstUser.Result.Id }
               );
                context.SaveChanges();
            }
        }

    }
}
