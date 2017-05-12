﻿using System;

namespace Repo.EF.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}