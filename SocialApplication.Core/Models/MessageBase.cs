using System;
using System.ComponentModel.DataAnnotations;

namespace SocialApplication.Core.Models
{
    public abstract class MessageBase
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
