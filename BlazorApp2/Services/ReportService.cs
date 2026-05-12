using BlazorApp2.Data;
using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Services
{
    public class ReportService : IReportService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public ReportService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Report>> GetLatestDeviceReportsAsync()
        {
            using var _db = await _dbFactory.CreateDbContextAsync();
            return await _db.Reports
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Report>> GetReportsByMaxReportIdAsync()
        {
            using var _db = await _dbFactory.CreateDbContextAsync();
            int? maxReportId = await _db.Reports.MaxAsync(r => (int?)r.ReportId);
            if (maxReportId == null)
                return new List<Report>();

            return await _db.Reports
                .Where(r => r.ReportId == maxReportId)
                .ToListAsync();
        }

        public async Task<List<Report>> GetLatestStateForAllNodesAsync()
        {
            using var _db = await _dbFactory.CreateDbContextAsync();

            // Получаем последнюю запись для каждого узла (по максимальному ReportId)
            var latestReports = await _db.Reports
                .GroupBy(r => r.NodeId)
                .Select(g => g.OrderByDescending(r => r.ReportId).FirstOrDefault())
                .ToListAsync();

            return latestReports.Where(r => r != null).Select(r => r!).ToList();
        }
    }
}