using Microsoft.Playwright;

namespace PlaywrightXunitTest.Example
{
    public class UnitTest1
    {
        /// <summary>
        /// This is a simple test that navigates to google.com and asserts the title
        /// Which using Playwright with out using Fixture
        /// To show project it working to start with
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test1()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var page = await browser.NewPageAsync();

            // Navigate to a website
            await page.GotoAsync("https://google.com");

            // Perform actions and assertions
            var title = await page.TitleAsync();
            Assert.Equal("Google", title);

            // Close the browser
            await browser.CloseAsync();

        }
    }
}