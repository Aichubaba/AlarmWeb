using System;
using BlazorApp2.Helpers;

namespace BlazorApp2.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int? ReportId { get; set; }
        public string? Group { get; set; }
        public string? Title { get; set; }
        public string? PvnNumber { get; set; }
        public string? Location { get; set; }
        public int? NodeId { get; set; }
        public string? ModelName { get; set; }
        public string? SerialFd { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Temperature { get; set; }
        public bool? Available { get; set; }
        public bool? Errors { get; set; }
        public string? ErrorsDescription { get; set; }
        public int? Uptime { get; set; }
        public int? Downtime { get; set; }
        public int? ReportCounter { get; set; }
        public long? CreatedAt { get; set; }
        public string? UptimeHr { get; set; }
        public string? DowntimeHr { get; set; }
        public long? PingAt { get; set; }
        public long? StartedAt { get; set; }
        public long? DisconnectedAt { get; set; }
        public long? AppStartedAt { get; set; }
        public int? QueueQuantity { get; set; }
        public long? LastReportAt { get; set; }

        // Вычисляемые свойства для удобного отображения
        public DateTime? CreatedAtDateTime => UnixTimeHelper.FromUnix(CreatedAt);
        public DateTime? PingAtDateTime => UnixTimeHelper.FromUnix(PingAt);
        public DateTime? StartedAtDateTime => UnixTimeHelper.FromUnix(StartedAt);
        public DateTime? DisconnectedAtDateTime => UnixTimeHelper.FromUnix(DisconnectedAt);
        public DateTime? AppStartedAtDateTime => UnixTimeHelper.FromUnix(AppStartedAt);
        public DateTime? LastReportAtDateTime => UnixTimeHelper.FromUnix(LastReportAt);
    }
}