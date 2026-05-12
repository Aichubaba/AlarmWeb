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
    public class SchemaServiceIntegrationTests
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
        public async Task GetAllSchemasAsync_ReturnsData()
        {
            // Arrange
            await using var context = CreateContext();
            // Предположим, что в базе уже есть схемы, либо создадим тестовые
            if (!await context.NodeSchemas.AnyAsync())
            {
                context.Nodes.Add(new Node { Id = 999, SerialFd = "FD999", Title = "TestNode" });
                context.NodeSchemas.Add(new NodeSchema { NodeId = 999 });
                await context.SaveChangesAsync();
            }

            var factoryMock = new Mock<IDbContextFactory<AppDbContext>>();
            factoryMock.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new AppDbContext(
                           new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(ConnectionString).Options));

            var service = new SchemaService(factoryMock.Object);

            // Act
            var schemas = await service.GetAllSchemasAsync();

            // Assert
            Assert.NotEmpty(schemas);
            Assert.All(schemas, s => Assert.NotNull(s.Fd)); // если есть связанный узел, Fd будет заполнен

            // Очистка тестовых данных
            var testSchema = schemas.FirstOrDefault(s => s.NodeId == 999);
            if (testSchema != null)
            {
                context.NodeSchemas.Remove(testSchema);
                var node = await context.Nodes.FindAsync(999);
                if (node != null) context.Nodes.Remove(node);
                await context.SaveChangesAsync();
            }
        }
    }
}