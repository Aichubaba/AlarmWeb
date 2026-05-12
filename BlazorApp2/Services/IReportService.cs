using BlazorApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetLatestDeviceReportsAsync();
        Task<List<Report>> GetReportsByMaxReportIdAsync();
        Task<List<Report>> GetLatestStateForAllNodesAsync();
    }
}