using Repo.EF;
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

        public IHttpActionResult Get()
        {
            var results = _repo.GetTopics();
            return Ok(results);
        }
    }
}
