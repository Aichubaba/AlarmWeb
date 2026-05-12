namespace BlazorApp2.Models
{
    public class NodeSchema
    {
        public int Id { get; set; }
        public int? NodeId { get; set; }
        public string? PlaceCode { get; set; }
        public string? Title { get; set; }
        public int? PvnNumber { get; set; }
        public string? NodeTitle { get; set; }
        public string? Fd { get; set; }
        public string? Location { get; set; }
        public string? SchemeNumber { get; set; }
        public byte[]? Image { get; set; }

        // Навигационное свойство на узел
        public Node? Node { get; set; }
    }
}