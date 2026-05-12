using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace BlazorApp2.Tests.E2ETests
{
    public class TicketsPageTests : BaseE2ETest
    {
        [Fact]
        public async Task TicketsPage_LoadsAndAppliesFilters()
        {
            await LoginAsync();
            await Page.ClickAsync("text=Заявки");
            // Ждём появления таблицы
            await Page.WaitForSelectorAsync("table", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });

            // Вводим фильтр по Node ID
            await Page.FillAsync("input[placeholder='Node ID']", "1163");
            await Page.ClickAsync("button:has-text('Применить')");

            // Небольшая пауза для обновления таблицы
            await Page.WaitForTimeoutAsync(1000);

            // Проверяем, что есть строки
            var rows = await Page.QuerySelectorAllAsync("table tbody tr");
            Assert.NotEmpty(rows);
        }

        [Fact]
        public async Task TicketsPage_OpenDetailsModal()
        {
            await LoginAsync();
            await Page.ClickAsync("text=Заявки");
            await Page.WaitForSelectorAsync("table", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });

            // Кликаем на иконку "глаз" в первой строке
            var firstEyeIcon = Page.Locator("table tbody tr:first-child button i.bi-eye");
            await firstEyeIcon.ClickAsync();

            // Ждём появления модального оверлея
            var modal = await Page.WaitForSelectorAsync(".modal-overlay", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            Assert.NotNull(modal);

            // Закрываем модалку
            await Page.ClickAsync(".modal-overlay button:has-text('ЗАКРЫТЬ')");
            // Ждём, что оверлей исчез
            await Page.WaitForSelectorAsync(".modal-overlay", new PageWaitForSelectorOptions { State = WaitForSelectorState.Hidden });
        }
    }
}