
# PlaywrightXunitTest Project

This project sets up a Playwright testing environment using xUnit and .NET 8. It includes configuration management using `appsettings.json`, user secrets, and environment variables.

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

You can also install these packages via the .NET CLI:

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



