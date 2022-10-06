using ForumAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace ForumAPI.Data.Repositories
{

    public interface ITopicsRepository
    {
        Task<Topics> GetAsync(int topicId);
        Task<List<Topics>> GetMultipleAsync();
        Task InsertAsync(Topics topic);
        Task UpdateAsync(Topics topic);
        Task DeleteAsync(Topics topic);
    }
    public class TopicsRepository : ITopicsRepository
    {
        private readonly ForumDBContext _dbContext;

        public TopicsRepository(ForumDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Topics> GetAsync(int topicId)
        {
            return await _dbContext.Topics.FirstOrDefaultAsync(o => o.Id == topicId);
        }

        public async Task<List<Topics>> GetMultipleAsync()
        {
            return await _dbContext.Topics.ToListAsync();
        }

        public async Task InsertAsync(Topics topic)
        {
            _dbContext.Topics.Add(topic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Topics topic)
        {
            _dbContext.Topics.Update(topic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Topics topic)
        {
            _dbContext.Topics.Remove(topic);
            await _dbContext.SaveChangesAsync();
        }
    }
}
