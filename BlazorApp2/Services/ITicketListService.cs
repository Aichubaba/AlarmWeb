using BlazorApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public interface ITicketListService
    {
        Task<int> GetTotalTicketsCountAsync();
        Task<List<TicketWithFd>> SearchByFdAsync(string search);
    }
}
