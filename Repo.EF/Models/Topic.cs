using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.EF.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
