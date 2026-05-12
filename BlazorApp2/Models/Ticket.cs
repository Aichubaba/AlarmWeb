using System;

namespace BlazorApp2.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? TicketStatus { get; set; }
        public int? NodeId { get; set; }
        public bool? Available { get; set; }
        public double? Temperature { get; set; }
        public bool? Errors { get; set; }
        public string? TypeError { get; set; }
        public string? ErrorsDescription { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? DisconnectedAt { get; set; }
        public int? QueueQty { get; set; }
        public DateTime? LastReportAt { get; set; }
        public bool? ManualStopped { get; set; }

        public string StatusText => TicketStatus switch
        {
            "0" => "Открыта",
            "1" => "Закрыта",
            "2" => "В ожидании",
            "3" => "В работе",
            "4" => "Приостановлена",
            "5" => "Отменена",
            _ => $"Статус {TicketStatus}"
        };
    }
}