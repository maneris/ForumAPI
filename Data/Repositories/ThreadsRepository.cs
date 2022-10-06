using System.Collections.Generic;
using ForumAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace ForumAPI.Data.Repositories
{
    public interface IThreadsRepository
    {
        Task<Threads> GetAsync(int topicId, int threadId);
        Task<List<Threads>> GetMultipleAsync(int topicId);
        Task InsertAsync(Threads thread);
        Task UpdateAsync(Threads thread);
        Task DeleteAsync(Threads thread);
    }

    public class ThreadsRepository : IThreadsRepository
    {
        private readonly ForumDBContext _dbContext;

        public ThreadsRepository(ForumDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Threads> GetAsync(int topicId, int threadId)
        {
            return await _dbContext.Threads.FirstOrDefaultAsync(o => o.TopicId == topicId && o.Id == threadId);
        }

        public async Task<List<Threads>> GetMultipleAsync(int topicId)
        {
            return await _dbContext.Threads.Where(o => o.TopicId == topicId).ToListAsync();
        }

        public async Task InsertAsync(Threads thread)
        {
            _dbContext.Threads.Add(thread);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Threads thread)
        {
            _dbContext.Threads.Update(thread);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Threads thread)
        {
            _dbContext.Threads.Remove(thread);
            await _dbContext.SaveChangesAsync();
        }
    }
}
