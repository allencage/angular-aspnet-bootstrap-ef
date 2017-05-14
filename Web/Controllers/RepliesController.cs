using Repo.EF;
using Repo.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class RepliesController : ApiController
    {
		private readonly IMessagesRepository _repo;

		public RepliesController(IMessagesRepository repo)
		{
			_repo = repo;
		}

		public IHttpActionResult Get(int topicId)
		{
			var results = _repo.GetRepliesPerTopic(topicId);
			return Ok(results);
		}

		public IHttpActionResult Get(int topicId, int id)
		{
			var results = _repo.GetReplyFromTopicById(topicId, id);
			return Ok(results);
		}

		public IHttpActionResult Post([FromUri]int topicId, [FromBody]Reply reply)
		{
			if (reply == null) return BadRequest();
			reply.TopicId = topicId;
			reply.Created = DateTime.UtcNow;

			_repo.AddReply(reply);
			if(!_repo.Commit()) return BadRequest();
			return Created("", reply);
		}
    }
}
