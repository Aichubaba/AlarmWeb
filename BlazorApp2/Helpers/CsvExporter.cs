using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorApp2.Helpers
{
    public static class CsvExporter
    {
        public static byte[] ExportToCsv<T>(IEnumerable<T> data, Func<T, string[]> selector, string[]? headers = null)
        {
            var sb = new StringBuilder();
            if (headers != null)
            {
                sb.AppendLine(string.Join(",", headers.Select(h => $"\"{h?.Replace("\"", "\"\"")}\"")));
            }
            foreach (var item in data)
            {
                var values = selector(item);
                sb.AppendLine(string.Join(",", values.Select(v => $"\"{v?.Replace("\"", "\"\"")}\"")));
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
