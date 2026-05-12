using BlazorApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public interface ITicketHistoryService
    {
        Task<List<TicketHistory>> GetHistoryForTicketAsync(int ticketId);
        Task LogActionAsync(int ticketId, string action, string description, string performedBy);
    }
}
