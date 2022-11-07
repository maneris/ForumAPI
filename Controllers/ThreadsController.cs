using ForumAPI.Auth.Model;
using ForumAPI.Data.Entities;
using ForumAPI.Data;
using ForumAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumAPI.Controllers
{
    [Route("api/v1/topics/{topicid}/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadsRepository _threadsRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IAuthorizationService _authorizationService;

        public ThreadsController(IThreadsRepository ThreadsRepository, ITopicsRepository TopicsRepository, IAuthorizationService AuthorizationService)
        {
            _threadsRepository = ThreadsRepository;
            _topicsRepository = TopicsRepository;
            _authorizationService = AuthorizationService;
        }
        /*
         *
            threads
           /api/v1/topics/{topicId}/threads GET ALL 200
           /api/v1/topics/{topicId}/threads/{id} GET 200
           /api/v1/topics/{topicId}/threads POST 201
           /api/v1/topics/{topicId}/threads/{id} PUT 200
           /api/v1/topics/{topicId}/threads/{id} DELETE 200    *
         */

        // GET: api/v1/topics/{topicId}/threads
        [HttpGet]
        [Authorize(Roles = ForumRoles.AnonGuest)]
        public async Task<IEnumerable<ThreadDto>> Get(int topicId)
        {
            var threads = await _threadsRepository.GetMultipleAsync(topicId);
            return threads.Select(o => new ThreadDto(o.Id, o.Title, o.Description, o.CreationDateTime));
        }

        // GET api/topics/{topicId}/threads/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = ForumRoles.AnonGuest)]
        public async Task<ActionResult<ThreadDto>> Get(int topicId,int id)
        {
            var threads = await _threadsRepository.GetAsync(topicId,id);
            // 404
            if (threads == null)
            {
                return NotFound();
            }
            //200
            return new ThreadDto(threads.Id, threads.Title, threads.Description, threads.CreationDateTime);
        }

        // POST api/v1/topics/{topicId}/threads
        [HttpPost]
        [Authorize(Roles = ForumRoles.AuthForumUser)]
        public async Task<ActionResult<ThreadDto>> Post(int topicId, Threads createThread)
        {
            Topics topic = await _topicsRepository.GetAsync(topicId);
            var thread = new Threads
            {
                Title = createThread.Title,
                Description = createThread.Description,
                CreationDateTime = DateTime.Now,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub),
                TopicId = topicId
            };
            await _threadsRepository.InsertAsync(createThread);
            return Created("", new ThreadDto(thread.Id,thread.Title,thread.Description,thread.CreationDateTime));

        }

        // PUT api/v1/topics/{topicId}/threads/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = ForumRoles.AuthForumUser)]
        public async Task<ActionResult<ThreadDto>> Put(int topicId,int id, UpdateThreadDto updateThread)
        {
            var thread = await _threadsRepository.GetAsync(topicId, id);

            // 404
            if (thread == null)
                return NotFound();
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, thread, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }
            thread.Description = updateThread.Description;
            await _threadsRepository.UpdateAsync(thread);

            return Ok(new ThreadDto(thread.Id, thread.Title, thread.Description, thread.CreationDateTime));
        }

        // DELETE api/v1/topics/{topicId}/threads/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = ForumRoles.AuthForumUser)]
        public async Task<ActionResult<ThreadDto>> Delete(int topicId,int id)
        {
            var thread = await _threadsRepository.GetAsync(topicId, id);

            // 404
            if (thread == null)
                return NotFound();
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, thread, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 403
                return Forbid();
            }
            await _threadsRepository.DeleteAsync(thread);


            // 204
            return NoContent();
        }
    }
}
