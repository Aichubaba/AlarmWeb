namespace BlazorApp2.Models
{
    public class EquipmentSummary
    {
        public int Id { get; set; }
        public int? Pvn { get; set; }
        public int? NodeId { get; set; }
        public string? Fd { get; set; }
        public string? Rubezh { get; set; }
        public string? Equipment { get; set; }
        public string? EquipmentType { get; set; }
        public string? Bu { get; set; }
        public string? AddressIp { get; set; }
        public string? Network { get; set; }
        public string? Gateway { get; set; }
        public string? Mask { get; set; }
    }
}