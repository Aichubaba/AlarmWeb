using System;

namespace BlazorApp2.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int? TicketId { get; set; }
        public long? CreatedAt { get; set; }   // bigint

        public DateTime? CreatedAtDateTime => CreatedAt.HasValue
            ? DateTimeOffset.FromUnixTimeSeconds(CreatedAt.Value).LocalDateTime : null;
    }
}