using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public class TicketHistoryService : ITicketHistoryService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly ILogger<TicketHistoryService> _logger;

        public TicketHistoryService(IDbContextFactory<AppDbContext> dbFactory, ILogger<TicketHistoryService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<List<TicketHistory>> GetHistoryForTicketAsync(int ticketId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.TicketHistories
                .Where(h => h.TicketId == ticketId)
                .OrderByDescending(h => h.PerformedAt)
                .ToListAsync();
        }

        public async Task LogActionAsync(int ticketId, string action, string description, string performedBy)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var history = new TicketHistory
            {
                TicketId = ticketId,
                Action = action,
                Description = description,
                PerformedBy = performedBy,
                PerformedAt = DateTime.UtcNow
            };
            db.TicketHistories.Add(history);
            await db.SaveChangesAsync();
            _logger.LogInformation("History record added for ticket {TicketId}: {Action}", ticketId, action);
        }
    }
}