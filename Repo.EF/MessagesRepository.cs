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

		public void AddTopic(Topic topic)
		{
			try
			{
				_context.Topics.Add(topic);
			}
			catch (Exception)
			{
				//ignore
			}
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

		public IEnumerable<Reply> GetReplies()
		{
			var results = _context.Replies.ToArray();
			return results;
		}

		public Reply GetReplyById(int id)
		{
			var result = _context.Replies.FirstOrDefault(r => r.Id == id);
			return result;
		}

		public Reply GetReplyFromTopicById(int topicId, int id)
		{
			var result = _context.Replies.FirstOrDefault(r => r.Id == id && r.TopicId == topicId);
			return result;
		}

		public void AddReply(Reply reply)
		{
			try
			{
				_context.Replies.Add(reply);
			}
			catch (Exception)
			{
				//ignore
			}
		}

		public bool Commit()
		{
			try
			{
				return _context.SaveChanges() > 0;
			}
			catch (Exception)
			{
				//ignore
				return false;
			}
		}
	}
}
