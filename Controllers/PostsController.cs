using Microsoft.AspNetCore.Mvc;
using ForumAPI.Data.Entities;
using ForumAPI.Data.Repositories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumAPI.Controllers
{
    [Route("api/v1/topics/{topicId}/threads/{threadId}/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        public PostsController(IPostsRepository PostsRepository)
        {
            _postsRepository = PostsRepository;
        }
        /*
         *
            posts
           /api/v1/topics/{topicId}/threads/{threadId}/posts GET ALL 200
           /api/v1/topics/{topicId}/threads/{threadId}/posts/{id} GET 200
           /api/v1/topics/{topicId}/threads/{threadId}/posts POST 201
           /api/v1/topics/{topicId}/threads/{threadId}/posts/{id} PUT 200
           /api/v1/topics/{topicId}/threads/{threadId}/posts/{id} DELETE 200/204     *
         */

        // GET: /api/v1/topics/{topicId}/threads/{threadId}/posts
        [HttpGet]
        public async Task<IEnumerable<Posts>> Get(int topicId, int threadId)
        {
            var posts = await _postsRepository.GetMultipleAsync(topicId, threadId);

            return posts;
        }

        // GET: /api/v1/topics/{topicId}/threads/{threadId}/posts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> Get(int topicId,int threadId, int id)
        {
            var posts = await _postsRepository.GetAsync(topicId, threadId,id);
            // 404
            if (posts == null)
            {
                return NotFound();
            }
            //200
            return posts;

        }

        // POST /api/v1/topics/{topicId}/threads/{threadId}/posts
        [HttpPost]
        public async Task<ActionResult<Posts>> Post(int topicId,int threadId, Posts createpost)
        {
            createpost.ThreadId =threadId;
            createpost.TopicId=topicId;
            createpost.CreationDate = DateTime.Now;
            await _postsRepository.InsertAsync(createpost);
            return Created("", createpost);
        }

        // PUT /api/v1/topics/{topicId}/threads/{threadId}/posts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Posts>> Put(int id, int topicId, int threadId,Posts updatePost)
        {
            var post = await _postsRepository.GetAsync(topicId,threadId,id);

            // 404
            if (post == null)
                return NotFound();

            post.Description = updatePost.Description;
            await _postsRepository.UpdateAsync(post);

            return Ok(post);
        }

        // DELETE /api/v1/topics/{topicId}/threads/{threadId}/posts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Posts>> Delete(int id, int topicId, int threadId)
        {
            var post = await _postsRepository.GetAsync(topicId,threadId,id);

            // 404
            if (post == null)
                return NotFound();

            await _postsRepository.DeleteAsync(post);


            // 204
            return NoContent();
        }
    }
}
