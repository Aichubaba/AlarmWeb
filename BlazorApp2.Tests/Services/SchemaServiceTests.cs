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
    public class SchemaServiceTests
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public SchemaServiceTests()
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
        public async Task GetAllSchemasAsync_ReturnsAllSchemasWithNodeData()
        {
            // Arrange
            await using var context = CreateContext();
            context.Nodes.Add(new Node { Id = 1, PvnNumber = 10, SerialFd = "FD01", Title = "Node1", Location = "Loc1" });
            context.Nodes.Add(new Node { Id = 2, PvnNumber = 20, SerialFd = "FD02", Title = "Node2", Location = "Loc2" });
            context.NodeSchemas.Add(new NodeSchema { Id = 1, NodeId = 1 });
            context.NodeSchemas.Add(new NodeSchema { Id = 2, NodeId = 2 });
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(_options));

            var service = new SchemaService(factoryMock.Object);

            // Act
            var schemas = await service.GetAllSchemasAsync();

            // Assert
            Assert.Equal(2, schemas.Count);
            Assert.Equal("FD01", schemas.First(s => s.NodeId == 1).Fd);
            Assert.Equal("Loc2", schemas.First(s => s.NodeId == 2).Location);
        }

        [Fact]
        public async Task AddSchemaAsync_AddsNewSchema()
        {
            // Arrange
            await using var context = CreateContext();
            context.Nodes.Add(new Node { Id = 1, PvnNumber = 10, SerialFd = "FD01", Title = "Node1" });
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new AppDbContext(_options));

            var service = new SchemaService(factoryMock.Object);

            // Act
            await service.AddSchemaAsync(10, new byte[] { 1, 2, 3 });

            // Assert – проверяем через новый контекст
            await using var assertContext = new AppDbContext(_options);
            var schema = assertContext.NodeSchemas.First();
            Assert.Equal(1, schema.NodeId);
            Assert.NotNull(schema.SchemeNumber);
            // Поле Fd может быть null, т.к. помечено как игнорируемое в EF, поэтому не проверяем его.
        }

        [Fact]
        public async Task GetNodesForDropdownAsync_ReturnsAllNodes()
        {
            // Arrange
            await using var context = CreateContext();
            context.Nodes.Add(new Node { Id = 1, PvnNumber = 10 });
            context.Nodes.Add(new Node { Id = 2, PvnNumber = 20 });
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(_options));

            var service = new SchemaService(factoryMock.Object);

            // Act
            var nodes = await service.GetNodesForDropdownAsync();

            // Assert
            Assert.Equal(2, nodes.Count);
        }

        [Fact]
        public async Task SearchSchemasAsync_FiltersByFd()
        {
            // Arrange
            await using var context = CreateContext();
            context.Nodes.Add(new Node { Id = 1, SerialFd = "FD001" });
            context.Nodes.Add(new Node { Id = 2, SerialFd = "FD002" });
            context.NodeSchemas.Add(new NodeSchema { Id = 1, NodeId = 1 });
            context.NodeSchemas.Add(new NodeSchema { Id = 2, NodeId = 2 });
            await context.SaveChangesAsync();

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(_options));

            var service = new SchemaService(factoryMock.Object);

            // Act
            var results = await service.SearchSchemasAsync("FD001", "");

            // Assert
            Assert.Single(results);
            Assert.Equal("FD001", results[0].Fd);
        }
    }
}