using System.Collections.Generic;
using Repo.EF.Models;

namespace Repo.EF
{
    public interface IMessagesRepository
    {
        IEnumerable<Reply> GetRepliesPerTopic(int topicId);
        Topic GetTopicById(int topicId);
        IEnumerable<Topic> GetTopics();
        IEnumerable<Topic> GetTopicsWithReplies();
    }
}