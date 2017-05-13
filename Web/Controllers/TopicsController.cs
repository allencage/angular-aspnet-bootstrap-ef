using Repo.EF;
using Repo.EF.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Web.Controllers
{
    public class TopicsController : ApiController
    {
        private readonly IMessagesRepository _repo;

        public TopicsController(IMessagesRepository repo)
        {
            _repo = repo;
        }

		//public IHttpActionResult Get()
		//{
		//    var results = _repo.GetTopicsWithReplies();
		//    return Ok(results);
		//}

		public IHttpActionResult Get(int id)
		{
			var results = _repo.GetTopicById(id);
			return Ok(results);
		}

		public IHttpActionResult GetWithReplies(bool replies = false)
		{
			var results = replies ? _repo.GetTopicsWithReplies() : _repo.GetTopics();
			return Ok(results);
		}

		public IHttpActionResult Post([FromBody]Topic topic)
		{
			topic.Created = DateTime.UtcNow;
			_repo.AddTopic(topic);
			if (!_repo.Commit()) return BadRequest();
			return Created<Topic>("", topic);
		}
    }
}
