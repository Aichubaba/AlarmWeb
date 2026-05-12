        namespace BlazorApp2.Models
{
    public class Node
    {
        public int Id { get; set; }
        public string? Group { get; set; }
        public int? PvnNumber { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? ModelName { get; set; }
        public bool? IgnoredInTask { get; set; }
        public string? SerialFd { get; set; }
        public string? FdIp { get; set; }
    }
}