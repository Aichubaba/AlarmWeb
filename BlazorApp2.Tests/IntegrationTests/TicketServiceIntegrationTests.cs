using BlazorApp2.Services;
using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApp2.Tests.IntegrationTests
{
    public class TicketServiceIntegrationTests
    {
        private const string ConnectionString = "Host=localhost;Database=postgres;Username=postgres;Password=130805";

        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(ConnectionString)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CloseTicketAsync_UpdatesStatusInDatabase()
        {
            // Arrange: создаём тестовую заявку с текущей датой
            await using var context = CreateContext();
            var ticket = new Ticket
            {
                TicketStatus = "0",
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(
                           new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(ConnectionString).Options));

            var logger = Mock.Of<ILogger<TicketService>>();
            var service = new TicketService(factoryMock.Object, logger);

            // Act
            await service.CloseTicketAsync(ticket.Id);

            // Assert: проверяем статус в новом контексте
            await using var assertContext = CreateContext();
            var updatedTicket = await assertContext.Tickets.FindAsync(ticket.Id);
            Assert.Equal("1", updatedTicket.TicketStatus);
            Assert.NotNull(updatedTicket.ClosedAt);

            // Очистка
            assertContext.Tickets.Remove(updatedTicket);
            await assertContext.SaveChangesAsync();
        }
    }
}