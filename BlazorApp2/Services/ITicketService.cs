using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public interface ITicketService
    {
        Task CloseTicketAsync(int ticketId);
        Task CloseTicketsAsync(List<int> ticketIds);
        Task UpdateTicketStatusAsync(int ticketId, string newStatus);
    }
}
