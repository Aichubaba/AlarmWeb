using System;

namespace BlazorApp2.Models
{
    public class TicketWithFd
    {
        public int Id { get; set; }
        public string? NodeFd { get; set; }
        public int? NodeId { get; set; }
        public string? NodeTitle { get; set; }
        public string? NodeGroup { get; set; }
        public string? NodeModelName { get; set; }
        public string? TypeError { get; set; }
        public int? QueueQty { get; set; }
        public string? TicketStatus { get; set; }
        public string? StatusText { get; set; }
        public double? Temperature { get; set; }
        public bool? Available { get; set; }
        public bool? Errors { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? NodeLocation { get; set; }
        public string? ErrorsDescription { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}