using ForumAPI.Data.Repositories;
using ForumAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ForumAPI.Auth.Model;
using ForumAPI.Data;

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
        public async Task<IEnumerable<TopicDto>> Get()
        {
            var threads = await _topicsRepository.GetMultipleAsync();
            return threads.Select(o => new TopicDto(o.Id,o.Title,o.Description,o.CreationDateTime));
        }

        // GET api/v1/topics/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = ForumRoles.AnonGuest)]

        public async Task<ActionResult<TopicDto>> Get( int id)
        {
            var topic = await _topicsRepository.GetAsync(id);
            // 404
            if (topic == null)
            {
                return NotFound();
            }
            //200
            return Ok(new TopicDto(topic.Id, topic.Title, topic.Description, topic.CreationDateTime));
        }

        // POST api/v1/topics
        [HttpPost]
        [Authorize(Roles = ForumRoles.Admin)]
        public async Task<ActionResult<TopicDto>> Post( CreateTopicDto createTopic)
        {
            
            var topic = new Topics {
                Title = createTopic.Title,
                Description= createTopic.Description,
                CreationDateTime = DateTime.Now
            };
            await _topicsRepository.InsertAsync(topic);
            return Created("", new TopicDto(topic.Id,topic.Title,topic.Description,topic.CreationDateTime));

        }

        // PUT api/v1/topics/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = ForumRoles.Admin)]
        public async Task<ActionResult<TopicDto>> Put(int id, UpdateTopicDto updateTopic)
        {
            var topic = await _topicsRepository.GetAsync(id);

            // 404
            if (topic == null)
                return NotFound();
            
            topic.Description = updateTopic.Description;
            await _topicsRepository.UpdateAsync(topic);

            return Ok(new TopicDto(topic.Id, topic.Title, topic.Description, topic.CreationDateTime));
        }

        // DELETE api/v1/topics/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = ForumRoles.Admin)]
        public async Task<ActionResult<TopicDto>> Delete( int id)
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
