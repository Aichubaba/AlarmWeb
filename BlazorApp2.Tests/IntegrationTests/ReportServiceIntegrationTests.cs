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
    public class ReportServiceIntegrationTests
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
        public async Task GetReportsByMaxReportIdAsync_ReturnsOnlyMaxReportIdRecords()
        {
            // Arrange
            await using var context = CreateContext();
            // Определяем текущий максимальный ReportId в базе
            int? currentMax = await context.Reports.MaxAsync(r => (int?)r.ReportId) ?? 0;

            // Добавляем тестовые данные с заведомо меньшим ReportId, чтобы не ломать существующие
            context.Reports.Add(new Report { ReportId = currentMax + 1, NodeId = 9999, CreatedAt = 1000 });
            context.Reports.Add(new Report { ReportId = currentMax + 2, NodeId = 9998, CreatedAt = 2000 });
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(
                           new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(ConnectionString).Options));

            var service = new ReportService(factoryMock.Object);

            // Act
            var results = await service.GetReportsByMaxReportIdAsync();

            // Assert – все записи должны иметь максимальный ReportId (currentMax + 2)
            Assert.NotEmpty(results);
            Assert.All(results, r => Assert.Equal(currentMax + 2, r.ReportId));

            // Очистка
            await using var cleanupContext = CreateContext();
            var toDelete = await cleanupContext.Reports
                .Where(r => r.NodeId == 9999 || r.NodeId == 9998)
                .ToListAsync();
            cleanupContext.Reports.RemoveRange(toDelete);
            await cleanupContext.SaveChangesAsync();
        }
    }
}