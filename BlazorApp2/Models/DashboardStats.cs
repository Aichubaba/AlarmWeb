using System.Collections.Generic;

namespace BlazorApp2.Models
{
    public class DashboardStats
    {
        public int TotalNodes { get; set; }
        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int ClosedTickets { get; set; }
        public List<TopProblemNode> TopProblemNodes { get; set; } = new();
    }

    public class TopProblemNode
    {
        public int NodeId { get; set; }
        public int TicketCount { get; set; }
    }
}