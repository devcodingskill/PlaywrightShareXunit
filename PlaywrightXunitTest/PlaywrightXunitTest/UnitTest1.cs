using Microsoft.Playwright;

namespace PlaywrightXunitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();

            // Navigate to a website
            await page.GotoAsync("https://example.com");

            // Perform actions and assertions
            var title = await page.TitleAsync();
            Assert.Equal("Example Domain", title);

            // Close the browser
            await browser.CloseAsync();

        }
    }
}