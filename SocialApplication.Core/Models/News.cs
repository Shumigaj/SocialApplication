using System;
using System.Collections.Generic;

namespace SocialApplication.Core.Models
{
    public class News : MessageBase
    {
        public string Title { get; set; }
        public bool AllowComments { get; set; } = true;
        public DateTime ModifiedAtUtc { get; set; }
        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
