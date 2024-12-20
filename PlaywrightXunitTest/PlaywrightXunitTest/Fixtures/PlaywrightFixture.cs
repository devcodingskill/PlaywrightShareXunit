﻿

using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace PlaywrightXunitTest.Fixtures
{
    public class PlaywrightFixture : IAsyncLifetime
    {
        //this class will get called before and after the test execution to setup the browser instance
        //and dispose of the browser instance after the test execution is completed but not disposed between each rthe tests
        //create the instance of the browser
        public IBrowser Browser { get; set; } = null!;
        private IPlaywright PlaywrightInstance { get; set; } = null!;
        public IBrowserContext BrowserContext { get; set; } = null!;
        public IConfiguration Configuration { get; set; } = null!;

        //implement the InitializeAsync method from IAsyncLifetime
        public async Task InitializeAsync()
        {
            PlaywrightInstance = await Playwright.CreateAsync();
            Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            BrowserContext = await Browser.NewContextAsync();
            Configuration = GetConfiguration();


            //this for testing the configuration values
            var username = Configuration["username"];
            var emailPrefix = Configuration[ "SsoEmailPrefix"];
            var EmailDomain = Configuration[ "SsoEmailDomain"];
            var Password = Configuration[ "SsoPassword"];
  
        }
        //implement the DisposeAsync method from IAsyncLifetime
        public async Task DisposeAsync()
        {
            await BrowserContext.DisposeAsync();
            await Browser.DisposeAsync();
            PlaywrightInstance.Dispose();
        }
        //Add Iconfiguration to get the configuration values when we setting up the test
        //To use the configuration we need to install
        //1.Microsoft.Extensions.Configuration package
        //2.Microsoft.Extensions.Configuration.FileExtensions package
        //3.Microsoft.Extensions.Configuration.Json package
        //4.Microsoft.Extensions.Configuration.EnvironmentVariables package

        //Add "appsettings.json" file to the project and set the properties to "Copy to output directory" to "Copy if newer"
        //add file to the project level so right-click on the project and add new item and select "appsettings.json" file

        //Add user secrets to the project to store the sensitive data like username and password
        //1 Need to install the Microsoft.Extensions.Configuration.UserSecrets package
        //2 Right-click on the project and select "Manage User Secrets" and add the sensitive data to the secrets.json file
        //3 Add the secrets.json file to the project level so right-click on the project and add existing item and select the secrets.json file
        //4 Make the class for model to call the secrets.json file in configuration => UserSecrets.cs in the Models folder
        //5 Add the UserSecrets class to the configuration in the PlaywrightFixture.cs file to get the configuration values
        //6 To check it works => add breakpoint in the InitializeAsync method and check the configuration values then run the test it will show the configuration values => Configuration["username"]
        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddUserSecrets<Models.UserSecrets>()
                 .AddEnvironmentVariables();


            return builder.Build();
        }
    }

    //Use CollectionDefinition attribute to apply the PlaywrightFixture to the test class => [CollectionDefinition("Playwright collection")]
    //To use it call Collection in test class  => [Collection("Playwright collection")]
    [CollectionDefinition("Playwright collection")]
    public class PlaywrightCollection : ICollectionFixture<PlaywrightFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
