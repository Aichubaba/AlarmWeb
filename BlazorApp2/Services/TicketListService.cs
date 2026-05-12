using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public class TicketListService : ITicketListService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly ILogger<TicketListService> _logger;
        private const int MaxRetries = 3;

        public TicketListService(IDbContextFactory<AppDbContext> dbFactory, ILogger<TicketListService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<int> GetTotalTicketsCountAsync()
        {
            for (int retry = 0; retry <= MaxRetries; retry++)
            {
                try
                {
                    using var db = await _dbFactory.CreateDbContextAsync();
                    return await db.Tickets.CountAsync();
                }
                catch (Exception ex) when (retry < MaxRetries)
                {
                    _logger.LogWarning(ex, "Retry {Retry} for GetTotalTicketsCount", retry);
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retry)));
                }
            }
            throw new Exception("Не удалось получить количество заявок после нескольких попыток");
        }

        public async Task<List<TicketWithFd>> SearchByFdAsync(string search)
        {
            for (int retry = 0; retry <= MaxRetries; retry++)
            {
                try
                {
                    using var db = await _dbFactory.CreateDbContextAsync();

                    var query = db.Tickets.AsQueryable();
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query = query.Where(t =>
                            (t.Id.ToString() != null && t.Id.ToString()!.Contains(search)) ||
                            (t.NodeId != null && t.NodeId.ToString()!.Contains(search)) ||
                            (t.TypeError != null && t.TypeError.Contains(search)));
                    }

                    var tickets = await query.ToListAsync();

                    var result = new List<TicketWithFd>();
                    foreach (var ticket in tickets)
                    {
                        var node = await db.Nodes.FirstOrDefaultAsync(n => n.Id == ticket.NodeId);
                        result.Add(new TicketWithFd
                        {
                            Id = ticket.Id,
                            NodeId = ticket.NodeId,
                            TicketStatus = ticket.TicketStatus,
                            Available = ticket.Available,
                            Errors = ticket.Errors,
                            TypeError = ticket.TypeError,
                            CreatedAt = ticket.CreatedAt,
                            LastUpdated = ticket.LastUpdated,
                            ClosedAt = ticket.ClosedAt,
                            Temperature = ticket.Temperature,
                            ErrorsDescription = ticket.ErrorsDescription,
                            StatusText = ticket.StatusText,
                            NodeFd = node?.SerialFd,
                            NodeTitle = node?.Title,
                            NodeGroup = node?.Group,
                            NodeModelName = node?.ModelName,
                            NodeLocation = node?.Location,
                        });
                    }

                    return result;
                }
                catch (Exception ex) when (retry < MaxRetries)
                {
                    _logger.LogWarning(ex, "Retry {Retry} for SearchByFd", retry);
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retry)));
                }
            }
            _logger.LogError("All retries exhausted for SearchByFd");
            throw new Exception("Не удалось загрузить список заявок после нескольких попыток");
        }
    }
}