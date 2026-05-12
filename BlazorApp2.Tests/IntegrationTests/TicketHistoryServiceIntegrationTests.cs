using BlazorApp2.Services;
using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApp2.Tests.IntegrationTests
{
    public class TicketHistoryServiceIntegrationTests
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
        public async Task LogActionAsync_AddsRecordToDatabase()
        {
            // Arrange
            await using var context = CreateContext();
            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new AppDbContext(
                    new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(ConnectionString).Options));

            var logger = Mock.Of<ILogger<TicketHistoryService>>();
            var service = new TicketHistoryService(factoryMock.Object, logger);

            // Act
            await service.LogActionAsync(9999, "TestAction", "TestDescription", "TestUser");

            // Assert
            await using var assertContext = CreateContext();
            var record = assertContext.TicketHistories.FirstOrDefault(h => h.TicketId == 9999);
            Assert.NotNull(record);
            Assert.Equal("TestAction", record.Action);

            // Очистка
            assertContext.TicketHistories.Remove(record);
            await assertContext.SaveChangesAsync();
        }
    }
}