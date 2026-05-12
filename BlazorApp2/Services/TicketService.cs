using BlazorApp2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public class TicketService : ITicketService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IDbContextFactory<AppDbContext> dbFactory, ILogger<TicketService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task CloseTicketAsync(int ticketId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var ticket = await db.Tickets.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.TicketStatus = "1";
                ticket.ClosedAt = DateTime.UtcNow;
                ticket.LastUpdated = DateTime.UtcNow;
                await db.SaveChangesAsync();
                _logger.LogInformation("Ticket {TicketId} closed", ticketId);
            }
        }

        public async Task CloseTicketsAsync(List<int> ticketIds)
        {
            if (ticketIds == null || ticketIds.Count == 0) return;
            using var db = await _dbFactory.CreateDbContextAsync();
            var tickets = await db.Tickets.Where(t => ticketIds.Contains(t.Id)).ToListAsync();
            foreach (var t in tickets)
            {
                t.TicketStatus = "1";
                t.ClosedAt = DateTime.UtcNow;
                t.LastUpdated = DateTime.UtcNow;
            }
            await db.SaveChangesAsync();
            _logger.LogInformation("Tickets {TicketIds} closed", string.Join(",", ticketIds));
        }

        public async Task UpdateTicketStatusAsync(int ticketId, string newStatus)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var ticket = await db.Tickets.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.TicketStatus = newStatus;
                if (newStatus == "1")
                {
                    ticket.ClosedAt = DateTime.UtcNow;
                }
                ticket.LastUpdated = DateTime.UtcNow;
                await db.SaveChangesAsync();
                _logger.LogInformation("Ticket {TicketId} status changed to {Status}", ticketId, newStatus);
            }
        }
    }
}