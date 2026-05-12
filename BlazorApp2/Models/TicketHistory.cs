using System;

namespace BlazorApp2.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string? Action { get; set; }
        public string? Description { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime? PerformedAt { get; set; }
    }
}
