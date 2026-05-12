using Microsoft.JSInterop;

namespace BlazorApp2.Helpers
{
    public class ChartJsHelper
    {
        private readonly IJSRuntime _jsRuntime;

        public ChartJsHelper(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task CreateLineChart(string canvasId, List<string> labels, List<int> data, string label = "Заявки")
        {
            var chartData = new
            {
                type = "line",
                data = new
                {
                    labels = labels,
                    datasets = new[]
                    {
                        new
                        {
                            label = label,
                            data = data,
                            borderColor = "rgb(75, 192, 192)",
                            tension = 0.1
                        }
                    }
                }
            };

            await _jsRuntime.InvokeVoidAsync("createChart", canvasId, chartData);
        }
    }
}
