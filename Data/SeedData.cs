using ForumAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace ForumAPI.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ForumDBContext(
            serviceProvider.GetRequiredService<DbContextOptions<ForumDBContext>>()))
            {
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
                new Threads { Title = "Newest nuclear reactor in France", Description="A discussion thread about the newest nuclear reactor built in France", CreationDateTime=DateTime.Now, TopicId=1 },
                new Threads { Title = "Mars discoveries", Description = "A discussion thread about the latest dicoveries on and about Mars", CreationDateTime = DateTime.Now, TopicId = 1 },
                new Threads { Title = "Animals being funny", Description = "A thread for videos of animals doing funny things", CreationDateTime = DateTime.Now, TopicId = 2 }
                );
                context.SaveChanges();
                context.Posts.AddRange(
               new Posts { Description = "Is it capabilities as good as the one in the Germany?",CreationDate=DateTime.Now,ThreadsId=1 },
               new Posts { Description = "Can it blow up from an earthquake?", CreationDate = DateTime.Now, ThreadsId = 1 },
               new Posts { Description = "There's been a new discovery of 10m diameter and 500m deep whole near mount Olimpus", CreationDate = DateTime.Now, ThreadsId = 2 }
               );
                context.SaveChanges();
            }
        }

    }
}
