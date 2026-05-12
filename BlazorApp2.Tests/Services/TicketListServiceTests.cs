using BlazorApp2.Services;
using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BlazorApp2.Tests.Services
{
    public class TicketListServiceTests
    {
        [Fact]
        public async Task GetTotalTicketsCountAsync_ReturnsCorrectCount()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new AppDbContext(options);
            context.Tickets.Add(new Ticket { Id = 1 });
            context.Tickets.Add(new Ticket { Id = 2 });
            await context.SaveChangesAsync();

            var mockFactory = new Mock<IDbContextFactory<AppDbContext>>();
            mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(context);

            var logger = Mock.Of<ILogger<TicketListService>>();
            var service = new TicketListService(mockFactory.Object, logger);

            var count = await service.GetTotalTicketsCountAsync();
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task SearchByFdAsync_EmptySearch_ReturnsAllTickets()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "SearchAllDb")
                .Options;

            using var context = new AppDbContext(options);
            context.Nodes.Add(new Node { Id = 1, SerialFd = "FD001", Title = "Node1" });
            context.Nodes.Add(new Node { Id = 2, SerialFd = "FD002", Title = "Node2" });
            context.Tickets.Add(new Ticket { Id = 1, NodeId = 1, TypeError = "GPS" });
            context.Tickets.Add(new Ticket { Id = 2, NodeId = 2, TypeError = "Error" });
            await context.SaveChangesAsync();

            var mockFactory = new Mock<IDbContextFactory<AppDbContext>>();
            mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(context);

            var logger = Mock.Of<ILogger<TicketListService>>();
            var service = new TicketListService(mockFactory.Object, logger);

            var results = await service.SearchByFdAsync("");

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public async Task SearchByFdAsync_SearchByTypeError_ReturnsFiltered()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "SearchTypeDb")
                .Options;

            using var context = new AppDbContext(options);
            context.Nodes.Add(new Node { Id = 1, SerialFd = "FD001", Title = "Node1" });
            context.Nodes.Add(new Node { Id = 2, SerialFd = "FD002", Title = "Node2" });
            context.Tickets.Add(new Ticket { Id = 1, NodeId = 1, TypeError = "GPS" });
            context.Tickets.Add(new Ticket { Id = 2, NodeId = 2, TypeError = "Error" });
            await context.SaveChangesAsync();

            var mockFactory = new Mock<IDbContextFactory<AppDbContext>>();
            mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(context);

            var logger = Mock.Of<ILogger<TicketListService>>();
            var service = new TicketListService(mockFactory.Object, logger);

            var results = await service.SearchByFdAsync("GPS");

            Assert.Single(results);
            Assert.Equal("FD001", results[0].NodeFd);
            Assert.Equal("Node1", results[0].NodeTitle);
        }
    }
}