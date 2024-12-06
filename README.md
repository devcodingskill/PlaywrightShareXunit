
# PlaywrightXunitTest Project

This project sets up a Playwright testing environment using xUnit and .NET 8. 
It includes configuration management using `appsettings.json`, user secrets, and environment variables.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for Playwright)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later

## Setup

### 1. Clone the Repository

### 2. Install NuGet Packages

Open the project in Visual Studio and install the following NuGet packages:

- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.FileExtensions`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.Configuration.EnvironmentVariables`
- `Microsoft.Extensions.Configuration.UserSecrets`
- `Microsoft.Playwright`


### 3. Add `appsettings.json`

Add an `appsettings.json` file to the project root with the following content:


Set the properties of `appsettings.json` to "Copy to Output Directory" -> "Copy if newer".

### 4. Configure User Secrets

1. Right-click on the project in Visual Studio and select "Manage User Secrets".
2. Add your sensitive data to the `secrets.json` file:


### 5. Set Environment Variables

Set environment variables in your operating system. For example, on Windows:


### 6. Update `PlaywrightFixture.cs`

Ensure your `PlaywrightFixture.cs` file looks like this:

```
  //implement the InitializeAsync method from IAsyncLifetime
    public async Task InitializeAsync()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        BrowserContext = await Browser.NewContextAsync();
        Configuration = GetConfiguration();

        //this for testing the configuration values
        var username = Configuration["username"];
        var emailPrefix = Configuration["SsoEmailPrefix"];
        var emailDomain = Configuration["SsoEmailDomain"];
        var password = Configuration["SsoPassword"];
    }

    //implement the DisposeAsync method from IAsyncLifetime
    public async Task DisposeAsync()
    {
        //for the page we close it in test class 
        //So we just Dispose BrowserContext and Browser only
        
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


//Use CollectionDefinition attribute to apply the PlaywrightFixture to the test class => [CollectionDefinition("Playwright collection")]
//To use it call Collection in test class  => [Collection("Playwright collection")]
[CollectionDefinition("Playwright collection")]
public class PlaywrightCollection : ICollectionFixture<PlaywrightFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}


```

### 7. Run the Tests

Step In the test class
    1. Add collection attribute this will call PlaywrightFixture to setup the browser instance and configuration values  
    2. Add to class IAsyncLifetime then implement the InitializeAsync and DisposeAsync.This will resposible to InitializeAsync and disposed after each test execution.
    3. Add field for the PlaywrightFixture and IPage to use in the test class
    4. Add the PlaywrightFixture to the test class constructor and inject the PlaywrightFixture to the test class
    5. Implement InitializeAsync method to setup the browser instance and configuration values
    6. Implement DisposeAsync method to dispose the browser instance and configuration values
# PlaywrightXunitTest Project

This project sets up a Playwright testing environment using xUnit and .NET 8. 
It includes configuration management using `appsettings.json`, user secrets, and environment variables.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for Playwright)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later

## Setup

### 1. Clone the Repository
### 2. Install NuGet Packages

Open the project in Visual Studio and install the following NuGet packages:

- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.Configuration.FileExtensions`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.Configuration.EnvironmentVariables`
- `Microsoft.Extensions.Configuration.UserSecrets`
- `Microsoft.Playwright`

### 3. Add `appsettings.json`

Add an `appsettings.json` file to the project root with the following content:
Set the properties of `appsettings.json` to "Copy to Output Directory" -> "Copy if newer".

### 4. Configure User Secrets

1. Right-click on the project in Visual Studio and select "Manage User Secrets".
2. Add your sensitive data to the `secrets.json` file:
### 5. Set Environment Variables

Set environment variables in your operating system. For example, on Windows:
### 6. Update `PlaywrightFixture.cs`

Ensure your `PlaywrightFixture.cs` file looks like this:
### 7. Run the Tests

In the test class:

1. Add the collection attribute to call `PlaywrightFixture` to set up the browser instance and configuration values.
2. Implement `IAsyncLifetime` and its methods `InitializeAsync` and `DisposeAsync`.
3. Add fields for `PlaywrightFixture` and `IPage` to use in the test class.
4. Inject `PlaywrightFixture` into the test class constructor.
5. Implement `InitializeAsync` to set up the browser instance and configuration values.
6. Implement `DisposeAsync` to dispose of the browser instance and configuration values.

```
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
```




   
Run the tests using Visual Studio Test Explorer or the .NET CLI:


## Additional Notes

- Ensure that the `appsettings.json` file is set to "Copy to Output Directory" -> "Copy if newer".
- Use the `Manage User Secrets` feature in Visual Studio to securely store sensitive data during development.
- Environment variables can be set in your operating system or CI/CD pipeline for different environments.

## Troubleshooting

- If you encounter issues with environment variables, ensure they are correctly set and accessible in your development environment.
- Check the output of the `Configuration` object in the `InitializeAsync` method to verify that the correct values are being loaded.

## License

This project is licensed under the MIT License.



