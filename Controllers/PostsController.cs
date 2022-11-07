using Microsoft.AspNetCore.Mvc;
using ForumAPI.Data.Entities;
using ForumAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using ForumAPI.Auth.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumAPI.Controllers
{
    [Route("api/v1/topics/{topicId}/threads/{threadId}/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IThreadsRepository _threadsRepository;
        private readonly IAuthorizationService _authorizationService;

        public PostsController(IPostsRepository PostsRepository, IThreadsRepository ThreadsRepository, IAuthorizationService AuthorizationService)
        {
            _postsRepository = PostsRepository;
            _threadsRepository = ThreadsRepository;
            _authorizationService = AuthorizationService;
        }
        /*
         *
            posts
           /api/v1/topics/{topicId}/threads/{threadId}/posts GET ALL 200
           /api/v1/topics/{topicId}/threads/{threadId}/posts/{id} GET 200
           /api/v1/topics/{topicId}/threads/{threadId}/posts POST 201
           /api/v1/topics/{topicId}/threads/{threadId}/posts/{id} PUT 200
           /api/v1/topics/{topicId}/threads/{threadId}/posts/{id} DELETE 200    *
         */

        // GET: /api/v1/topics/{topicId}/threads/{threadId}/posts
        [HttpGet]
        [Authorize(Roles = ForumRoles.AnonGuest)]
        public async Task<IEnumerable<Posts>> Get(int topicId, int threadId)
        {
            var posts = await _postsRepository.GetMultipleAsync(topicId, threadId);

            return posts;
        }

        // GET: /api/v1/topics/{topicId}/threads/{threadId}/posts/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = ForumRoles.AnonGuest)]
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
        [Authorize(Roles = ForumRoles.AuthForumUser)]
        public async Task<ActionResult<Posts>> Post(int topicId,int threadId, Posts createpost)
        {
            Threads thread = await _threadsRepository.GetAsync(topicId,threadId);
            createpost.Thread = thread;
            createpost.CreationDate = DateTime.Now;
            createpost.UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            await _postsRepository.InsertAsync(createpost);
            createpost.Thread = null;
            return Created("", createpost);
        }

        // PUT /api/v1/topics/{topicId}/threads/{threadId}/posts/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = ForumRoles.AuthForumUser)]
        public async Task<ActionResult<Posts>> Put(int id, int topicId, int threadId,Posts updatePost)
        {
            var post = await _postsRepository.GetAsync(topicId,threadId,id);

            // 404
            if (post == null)
                return NotFound();
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }
            post.Description = updatePost.Description;
            await _postsRepository.UpdateAsync(post);

            return Ok(post);
        }

        // DELETE /api/v1/topics/{topicId}/threads/{threadId}/posts/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = ForumRoles.AuthForumUser)]
        public async Task<ActionResult<Posts>> Delete(int id, int topicId, int threadId)
        {
            var post = await _postsRepository.GetAsync(topicId,threadId,id);

            // 404
            if (post == null)
                return NotFound();
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }
            await _postsRepository.DeleteAsync(post);


            // 204
            return NoContent();
        }
    }
}
