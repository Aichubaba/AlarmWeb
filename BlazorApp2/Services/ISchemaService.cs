using BlazorApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public interface ISchemaService
    {
        Task<List<NodeSchema>> GetAllSchemasAsync();
        Task<List<NodeSchema>> SearchSchemasAsync(string fd, string pvn);
        Task<List<Node>> GetNodesForDropdownAsync();
        Task AddSchemaAsync(int pvnNumber, byte[] image);
    }
}