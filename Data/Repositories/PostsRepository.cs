using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Data.Repositories
{

    public interface IPostsRepository
    {
        Task<Posts> GetAsync(int topicId,int threadId, int postId);
        Task<List<Posts>> GetMultipleAsync(int topicId, int threadId);
        Task InsertAsync(Posts post);
        Task UpdateAsync(Posts post);
        Task DeleteAsync(Posts post);
    }

    public class PostsRepository : IPostsRepository
    {
        private readonly ForumDBContext _dbContext;

        public PostsRepository(ForumDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Posts> GetAsync(int topicId, int threadId,int postId)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(o => o.Thread.Id == threadId && o.Thread.Topic.Id == topicId && o.Id == postId);
        }

        public async Task<List<Posts>> GetMultipleAsync(int topicId, int threadId)
        {
            return await _dbContext.Posts.Where(o => o.Thread.Id == threadId && o.Thread.Topic.Id == topicId ).ToListAsync();
        }

        public async Task InsertAsync(Posts post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Posts post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Posts post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
