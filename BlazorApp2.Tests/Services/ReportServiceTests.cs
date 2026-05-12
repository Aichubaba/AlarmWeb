using BlazorApp2.Services;
using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApp2.Tests.Services
{
    public class ReportServiceTests
    {
        // Сохраняем DbContextOptions для создания новых контекстов
        private readonly DbContextOptions<AppDbContext> _options;

        public ReportServiceTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private AppDbContext CreateContext()
        {
            var context = new AppDbContext(_options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetReportsByMaxReportIdAsync_ReturnsOnlyMaxReportIdRecords()
        {
            // Arrange
            await using var context = CreateContext();
            context.Reports.AddRange(
                new Report { Id = 1, ReportId = 1, NodeId = 1, CreatedAt = 1000 },
                new Report { Id = 2, ReportId = 1, NodeId = 2, CreatedAt = 2000 },
                new Report { Id = 3, ReportId = 2, NodeId = 1, CreatedAt = 3000 }
            );
            await context.SaveChangesAsync();

            // Фабрика, возвращающая новый контекст при каждом вызове
            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(_options));

            var service = new ReportService(factoryMock.Object);

            // Act
            var result = await service.GetReportsByMaxReportIdAsync();

            // Assert – ожидаем только записи с ReportId = 2 (максимальный)
            Assert.Single(result);
            Assert.Equal(2, result[0].ReportId);
            Assert.Equal(3, result[0].Id);
        }

        [Fact]
        public async Task GetLatestStateForAllNodesAsync_ReturnsLatestPerNode()
        {
            // Arrange
            await using var context = CreateContext();
            context.Reports.AddRange(
                new Report { Id = 1, ReportId = 1, NodeId = 1, CreatedAt = 1000 },
                new Report { Id = 2, ReportId = 2, NodeId = 1, CreatedAt = 2000 },
                new Report { Id = 3, ReportId = 2, NodeId = 2, CreatedAt = 1500 }
            );
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(_options));

            var service = new ReportService(factoryMock.Object);

            // Act
            var result = await service.GetLatestStateForAllNodesAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.NodeId == 1 && r.ReportId == 2);
            Assert.Contains(result, r => r.NodeId == 2 && r.ReportId == 2);
        }

        [Fact]
        public async Task GetLatestDeviceReportsAsync_ReturnsAllReportsOrdered()
        {
            // Arrange
            await using var context = CreateContext();
            context.Reports.AddRange(
                new Report { Id = 1, CreatedAt = 1000 },
                new Report { Id = 2, CreatedAt = 3000 },
                new Report { Id = 3, CreatedAt = 2000 }
            );
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(_options));

            var service = new ReportService(factoryMock.Object);

            // Act
            var result = await service.GetLatestDeviceReportsAsync();

            // Assert – порядок по убыванию CreatedAt
            Assert.Equal(3, result.Count);
            Assert.Equal(2, result[0].Id);
            Assert.Equal(3, result[1].Id);
            Assert.Equal(1, result[2].Id);
        }
    }
}