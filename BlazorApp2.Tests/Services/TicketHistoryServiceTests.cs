using BlazorApp2.Services;
using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Tests.Services
{
    public class TicketHistoryServiceTests
    {
        [Fact]
        public async Task LogActionAsync_AddsHistoryRecord()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "LogActionDb")
                .Options;

            await using var context = new AppDbContext(options);
            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new AppDbContext(options));

            var logger = Mock.Of<ILogger<TicketHistoryService>>();
            var service = new TicketHistoryService(factoryMock.Object, logger);

            await service.LogActionAsync(1, "Test", "Description", "Admin");

            // Используем новый контекст для проверки
            await using var assertContext = new AppDbContext(options);
            var history = await assertContext.TicketHistories.SingleAsync();
            Assert.Equal(1, history.TicketId);
            Assert.Equal("Test", history.Action);
            Assert.Equal("Admin", history.PerformedBy);
        }

        [Fact]
        public async Task GetHistoryForTicketAsync_ReturnsOrderedRecords()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetHistoryDb")
                .Options;

            await using var context = new AppDbContext(options);
            context.TicketHistories.Add(new TicketHistory { TicketId = 1, Action = "First", PerformedAt = System.DateTime.UtcNow.AddDays(-1) });
            context.TicketHistories.Add(new TicketHistory { TicketId = 1, Action = "Second", PerformedAt = System.DateTime.UtcNow });
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(context);

            var logger = Mock.Of<ILogger<TicketHistoryService>>();
            var service = new TicketHistoryService(factoryMock.Object, logger);

            var history = await service.GetHistoryForTicketAsync(1);

            Assert.Equal(2, history.Count);
            Assert.Equal("First", history[1].Action); // порядок: старшие сверху
            Assert.Equal("Second", history[0].Action);
        }
    }
}