﻿using ForumAPI.Data.Entities;
using ForumAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumAPI.Controllers
{
    [Route("api/v1/topics/{topicid}/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadsRepository _threadsRepository;
        public ThreadsController(IThreadsRepository ThreadsRepository)
        {
            _threadsRepository = ThreadsRepository;
        }
        /*
         *
            threads
           /api/v1/topics/{topicId}/threads GET ALL 200
           /api/v1/topics/{topicId}/threads/{id} GET 200
           /api/v1/topics/{topicId}/threads POST 201
           /api/v1/topics/{topicId}/threads/{id} PUT 200
           /api/v1/topics/{topicId}/threads/{id} DELETE 200/204     *
         */

        // GET: api/v1/topics/{topicId}/threads
        [HttpGet]
        public async Task<IEnumerable<Threads>> Get(int topicId)
        {
            var threads = await _threadsRepository.GetMultipleAsync(topicId);
            return threads;
        }

        // GET api/topics/{topicId}/threads/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Threads>> Get(int topicId,int id)
        {
            var threads = await _threadsRepository.GetAsync(topicId,id);
            // 404
            if (threads == null)
            {
                return NotFound();
            }
            //200
            return threads;
        }

        // POST api/v1/topics/{topicId}/threads
        [HttpPost]
        public async Task<ActionResult<Threads>> Post(int topicId, Threads createThread)
        {
            createThread.TopicId = topicId;
            createThread.CreationDateTime = DateTime.Now;
            await _threadsRepository.InsertAsync(createThread);
            return Created("", createThread);

        }

        // PUT api/v1/topics/{topicId}/threads/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Threads>> Put(int topicId,int id, Threads updateThread)
        {
            var thread = await _threadsRepository.GetAsync(topicId, id);

            // 404
            if (thread == null)
                return NotFound();

            thread.Description = updateThread.Description;
            await _threadsRepository.UpdateAsync(thread);

            return Ok(thread);
        }

        // DELETE api/v1/topics/{topicId}/threads/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Threads>> Delete(int topicId,int id)
        {
            var thread = await _threadsRepository.GetAsync(topicId, id);

            // 404
            if (thread == null)
                return NotFound();

            await _threadsRepository.DeleteAsync(thread);


            // 204
            return NoContent();
        }
    }
}
