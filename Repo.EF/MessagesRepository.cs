using Repo.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.EF
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly MessageBoardContext _context;

        public MessagesRepository(MessageBoardContext context)
        {
            _context = context;
        }

        public IEnumerable<Topic> GetTopics()
        {
            var results = _context.Topics.ToArray();
            return results;
        }

        public IEnumerable<Reply> GetRepliesPerTopic(int topicId)
        {
            var results = _context.Replies.Where(t => t.TopicId == topicId).ToArray();
            return results;
        }

        public Topic GetTopicById(int topicId)
        {
            var result = _context.Topics.Include("Replies").SingleOrDefault(t => t.Id == topicId);
            return result;
        }

        public IEnumerable<Topic> GetTopicsWithReplies()
        {
            var results = _context.Topics.Include("Replies").ToArray();
            return results;
        }
    }
}
