using System.Collections.Generic;
using Repo.EF.Models;

namespace Repo.EF
{
	public interface IMessagesRepository
	{

		void AddTopic(Topic topic);
		IEnumerable<Reply> GetRepliesPerTopic(int topicId);
		Topic GetTopicById(int topicId);
		IEnumerable<Topic> GetTopics();
		IEnumerable<Topic> GetTopicsWithReplies();
		IEnumerable<Reply> GetReplies();
		Reply GetReplyById(int id);
		Reply GetReplyFromTopicById(int topicId, int id);
		bool Commit();
	}
}