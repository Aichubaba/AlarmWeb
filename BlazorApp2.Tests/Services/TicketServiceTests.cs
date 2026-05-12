using BlazorApp2.Services;
using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BlazorApp2.Tests.Services
{
    public class TicketServiceTests
    {
        [Fact]
        public async Task CloseTicketAsync_ChangesStatusToClosed()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CloseTicketDb")
                .Options;

            await using var context = new AppDbContext(options);
            var ticket = new Ticket { Id = 1, TicketStatus = "0" };
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(context);

            var logger = Mock.Of<ILogger<TicketService>>();
            var service = new TicketService(factoryMock.Object, logger);

            await service.CloseTicketAsync(1);

            var updated = await context.Tickets.FindAsync(1);
            Assert.Equal("1", updated.TicketStatus);
            Assert.NotNull(updated.ClosedAt);
        }

        [Fact]
        public async Task UpdateTicketStatusAsync_ChangesStatusAndSetsClosedAt()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateStatusDb")
                .Options;

            await using var context = new AppDbContext(options);
            var ticket = new Ticket { Id = 1, TicketStatus = "0" };
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(context);

            var logger = Mock.Of<ILogger<TicketService>>();
            var service = new TicketService(factoryMock.Object, logger);

            await service.UpdateTicketStatusAsync(1, "1");

            var updated = await context.Tickets.FindAsync(1);
            Assert.Equal("1", updated.TicketStatus);
            Assert.NotNull(updated.ClosedAt);
        }
    }
}