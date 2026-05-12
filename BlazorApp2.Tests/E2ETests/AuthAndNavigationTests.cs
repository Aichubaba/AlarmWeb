using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApp2.Tests.E2ETests
{
    public class AuthAndNavigationTests : BaseE2ETest
    {
        [Fact]
        public async Task Login_AsAdmin_ShowsAdminButton()
        {
            await LoginAsync();

            // Переходим на страницу поиска схем
            await Page.GotoAsync($"{BaseUrl}/searchschemes");
            await Page.WaitForSelectorAsync("h3:has-text('Поиск схем')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });

            // Проверяем видимость кнопки добавления схемы
            var addButton = Page.Locator("button:has-text('ДОБАВИТЬ СХЕМУ')");
            Assert.True(await addButton.IsVisibleAsync());
        }

        [Fact]
        public async Task Navigation_AllMenuItems_LoadCorrectPages()
        {
            await LoginAsync();

            // "Заявки"
            await Page.ClickAsync("text=Заявки");
            await Page.WaitForSelectorAsync("h3:has-text('Заявки')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var ticketsHeader = await Page.QuerySelectorAsync("h3");
            Assert.NotNull(ticketsHeader);

            // "Последний отчёт"
            await Page.ClickAsync("text=Последний отчёт");
            await Page.WaitForSelectorAsync("h3:has-text('Отчёт №')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var reportHeader = await Page.QuerySelectorAsync("h3");
            Assert.NotNull(reportHeader);

            // "Оборудование"
            await Page.ClickAsync("text=Оборудование");
            await Page.WaitForSelectorAsync("h3:has-text('Оборудование')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var equipHeader = await Page.QuerySelectorAsync("h3");
            Assert.NotNull(equipHeader);

            // "Узлы"
            await Page.ClickAsync("text=Узлы");
            await Page.WaitForSelectorAsync("h3:has-text('Список Node')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var nodesHeader = await Page.QuerySelectorAsync("h3");
            Assert.NotNull(nodesHeader);

            // "Поиск схем"
            await Page.ClickAsync("text=Поиск схем");
            await Page.WaitForSelectorAsync("h3:has-text('Поиск схем')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var searchHeader = await Page.QuerySelectorAsync("h3");
            Assert.NotNull(searchHeader);
        }
    }
}