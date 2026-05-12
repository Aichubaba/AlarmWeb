using System;

namespace BlazorApp2.Models
{
    public class ReportStat
    {
        public int Id { get; set; }
        public int? NumberReport { get; set; }
        public long? UploadedAt { get; set; }   // bigint
        public int? CountRecords { get; set; }
        public bool? Processed { get; set; }
        public long? ProcessedAt { get; set; }  // bigint

        public DateTime? UploadedAtDateTime => UploadedAt.HasValue
            ? DateTimeOffset.FromUnixTimeSeconds(UploadedAt.Value).LocalDateTime : null;
        public DateTime? ProcessedAtDateTime => ProcessedAt.HasValue
            ? DateTimeOffset.FromUnixTimeSeconds(ProcessedAt.Value).LocalDateTime : null;
    }
}