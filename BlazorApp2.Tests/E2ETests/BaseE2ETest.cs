using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApp2.Tests.E2ETests
{
    public class BaseE2ETest : IAsyncLifetime
    {
        protected IPlaywright Playwright { get; private set; } = null!;
        protected IBrowser Browser { get; private set; } = null!;
        protected IPage Page { get; private set; } = null!;

        protected const string BaseUrl = "http://localhost:5159";

        public async Task InitializeAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
                SlowMo = 50
            });
            Page = await Browser.NewPageAsync();
        }

        protected async Task LoginAsync()
        {
            // Отправляем POST-запрос к API логина
            await Page.GotoAsync(BaseUrl);
            await Page.EvaluateAsync($@"
                fetch('{BaseUrl}/api/auth/login', {{
                    method: 'POST',
                    headers: {{ 'Content-Type': 'application/x-www-form-urlencoded' }},
                    body: 'username=admin&password=admin123&ReturnUrl=%2F',
                    credentials: 'include'
                }});
            ");
            // Переходим на главную страницу
            await Page.GotoAsync($"{BaseUrl}/");
            // Ждём появления заголовка страницы
            await Page.WaitForSelectorAsync("h3:has-text('Главная панель')", new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });
        }

        public async Task DisposeAsync()
        {
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}