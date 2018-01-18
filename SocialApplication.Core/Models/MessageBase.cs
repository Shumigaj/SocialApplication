using System;

namespace SocialApplication.Core.Models
{
    public abstract class MessageBase
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
