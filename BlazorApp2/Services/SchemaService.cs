using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public class SchemaService : ISchemaService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public SchemaService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Node>> GetNodesForDropdownAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Nodes.ToListAsync();
        }

        public async Task<List<NodeSchema>> GetAllSchemasAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var schemas = await db.NodeSchemas
                .Include(s => s.Node)          // подгружаем связанный узел
                .ToListAsync();

            // Заполняем поля из Node, так как они игнорируются EF
            foreach (var schema in schemas)
            {
                schema.PvnNumber = schema.Node?.PvnNumber;
                schema.NodeTitle = schema.Node?.Title;
                schema.Fd = schema.Node?.SerialFd;
                schema.Location = schema.Node?.Location;
            }

            return schemas;
        }

        public async Task<List<NodeSchema>> SearchSchemasAsync(string fd, string pvn)
        {
            // Этот метод больше не используется на странице, оставлен для совместимости
            using var db = await _dbFactory.CreateDbContextAsync();
            var schemas = await db.NodeSchemas
                .Include(s => s.Node)
                .ToListAsync();

            foreach (var schema in schemas)
            {
                schema.PvnNumber = schema.Node?.PvnNumber;
                schema.NodeTitle = schema.Node?.Title;
                schema.Fd = schema.Node?.SerialFd;
                schema.Location = schema.Node?.Location;
            }

            var result = schemas.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(fd))
                result = result.Where(s => s.Fd != null && s.Fd.Contains(fd, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(pvn) && int.TryParse(pvn, out int pvnNum))
                result = result.Where(s => s.PvnNumber == pvnNum);

            return result.ToList();
        }

        public async Task AddSchemaAsync(int pvnNumber, byte[] imageBytes)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var node = await db.Nodes.FirstOrDefaultAsync(n => n.PvnNumber == pvnNumber);
            if (node == null) return;

            var schema = new NodeSchema
            {
                PvnNumber = node.PvnNumber,
                NodeId = node.Id,
                Title = node.Title,
                Fd = node.SerialFd,
                Location = node.Location,
                Image = imageBytes,
                SchemeNumber = DateTime.Now.ToString("yyyyMMddHHmmss")
            };

            db.NodeSchemas.Add(schema);
            await db.SaveChangesAsync();
        }
    }
}