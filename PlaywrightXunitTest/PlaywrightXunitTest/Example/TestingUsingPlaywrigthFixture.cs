using PlaywrightXunitTest.Fixtures;

namespace PlaywrightXunitTest.Example
{
    
    //Use Collection attribute to apply the PlaywrightFixture to the test class
    //Inherit the IAsyncLifetime to implement the InitializeAsync and DisposeAsync methods
    [Collection("Playwright collection")]
    public class TestingUsingPlaywrigthFixture : IAsyncLifetime
    {
        //create field for the PlaywrightFixture and IPage
        private readonly PlaywrightFixture _playwrightFixture;
        private IPage _page;

        //inject the PlaywrightFixture to the constructor (this will automaticly from collection)
        public TestingUsingPlaywrigthFixture(PlaywrightFixture playwrightFixture)
        {
            _playwrightFixture = playwrightFixture;
            _page = null!;//initialize the page to avoid warning
        }
        //implement the DisposeAsync method from IAsyncLifetime
        public async Task DisposeAsync()
        {
            if (_page != null)
                await _page.CloseAsync();
        }
        //implement the InitializeAsync method from IAsyncLifetime
        public async Task InitializeAsync()
        {
            _page = await _playwrightFixture.BrowserContext.NewPageAsync();
        }

        [Fact]
        public async Task Test1()
        {
            await _page.GotoAsync("https://google.com");
            
            var title = await _page.TitleAsync();

            Assert.Equal("Google", title);
        }

        [Fact]
        public async Task ApplyCheckTitle()
        {
            
            await _page.GotoAsync("https://www.2apply.com.au/login");
           
            var title = await _page.TitleAsync();

            Assert.Equal("2Apply Free Fast Rental Application Form", title);
        }
    }
}