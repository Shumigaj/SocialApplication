using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialApplication.Core.Models
{
    public class News : MessageBase
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public bool AllowComments { get; set; } = true;

        public DateTime? ModifiedAtUtc { get; set; }

        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
