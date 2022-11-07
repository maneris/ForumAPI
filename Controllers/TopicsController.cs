using ForumAPI.Data.Repositories;
using ForumAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ForumAPI.Auth.Model;

namespace ForumAPI.Controllers
{
    [Route("api/v1/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsRepository _topicsRepository;
        private readonly IAuthorizationService _authorizationService;
        public TopicsController(ITopicsRepository TopicsRepository, IAuthorizationService AuthorizationService)
        {
            _topicsRepository = TopicsRepository;
            _authorizationService = AuthorizationService;
        }


        /*
         *
            topic
           /api/topics GET ALL 200
           /api/topics/{id} GET 200
           /api/topics POST 201
           /api/topics/{id} PUT 200
           /api/topics/{id} DELETE 200     *
         */

        // GET: api/v1/topics/
        [HttpGet]
        [Authorize(Roles = ForumRoles.AnonGuest)]
        public async Task<IEnumerable<Topics>> Get()
        {
            var threads = await _topicsRepository.GetMultipleAsync();
            return threads;
        }

        // GET api/v1/topics/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = ForumRoles.AnonGuest)]

        public async Task<ActionResult<Threads>> Get( int id)
        {
            var topic = await _topicsRepository.GetAsync(id);
            // 404
            if (topic == null)
            {
                return NotFound();
            }
            //200
            return Ok(topic);
        }

        // POST api/v1/topics
        [HttpPost]
        [Authorize(Roles = ForumRoles.Admin)]
        public async Task<ActionResult<Topics>> Post( Topics createTopic)
        {
            createTopic.CreationDateTime = DateTime.Now;
            await _topicsRepository.InsertAsync(createTopic);
            return Created("", createTopic);

        }

        // PUT api/v1/topics/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = ForumRoles.Admin)]
        public async Task<ActionResult<Topics>> Put(int id, Topics updateTopic)
        {
            var topic = await _topicsRepository.GetAsync(id);

            // 404
            if (topic == null)
                return NotFound();
            
            topic.Description = updateTopic.Description;
            await _topicsRepository.UpdateAsync(topic);

            return Ok(topic);
        }

        // DELETE api/v1/topics/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = ForumRoles.Admin)]
        public async Task<ActionResult<Topics>> Delete( int id)
        {
            var topic = await _topicsRepository.GetAsync( id);

            // 204
            if (topic == null)
                return NoContent();

            await _topicsRepository.DeleteAsync(topic);


            // 204
            return NoContent();
        }

    }
}
