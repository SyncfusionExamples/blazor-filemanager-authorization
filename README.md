# Blazor-filemanager-authorization

## Overview

This sample demonstrates role-based authorization using the Syncfusion [Blazor File Manager](https://www.syncfusion.com/blazor-components/blazor-file-manager) component in a Blazor Server application. Users are authenticated through ASP.NET Core Identity and are granted different file management capabilities based on their assigned roles.

The application dynamically configures available File Manager commands for different user types and additionally enforces role validation on the server before performing file system operations. This approach illustrates how UI-level restrictions can be combined with server-side authorization logic to create a more secure file management experience.

## Key Features

- Uses ASP.NET Core Identity authentication and role management.
- Retrieves authentication information using `AuthenticationState`.
- Checks user permissions through `User.IsInRole("Admin")` and `User.IsInRole("Employee")`.
- Dynamically configures File Manager toolbar commands according to the authenticated user's role.
- Uses `Syncfusion.EJ2.FileManager.PhysicalFileProvider` for file system access.
- Implements the `FileOperations` endpoint to process File Manager requests.
- Supports file and folder creation through the `Create()` method.
- Supports file and folder deletion through the `Delete()` method.
- Supports copy operations through the `Copy()` method.
- Supports move operations through the `Move()` method.
- Supports rename operations through the `Rename()` method.
- Supports file browsing through the `GetFiles()` method.
- Supports searching through the `Search()` method.
- Supports metadata retrieval through the `Details()` method.
- Supports uploads through the `Upload()` method.
- Supports downloads through the `Download()` method.
- Supports thumbnail and image retrieval through the `GetImage()` method.
- Stores managed content under the `wwwroot/Files` directory.

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet) compatible with the project's target framework

## How to Run the Project

**Visual Studio 2022**

1. Clone or download this repository.
2. Open `Blazor_Authentication.sln`.
3. Restore all NuGet packages.
4. Ensure `Blazor_Authentication` is set as the startup project.
5. Verify the connection string in `appsettings.json`.
6. Build the solution.
7. Run the application using `Ctrl+F5`.
8. Register or sign in with an ASP.NET Core Identity account and test role-based File Manager permissions.

**Visual Studio Code**

1. Open the repository folder in Visual Studio Code.
2. Navigate to the project directory containing `Blazor_Authentication.csproj`.

```bash
dotnet restore
```

```bash
dotnet run
```

3. Open the local URL displayed in the console after the application starts.
4. Sign in and verify role-based File Manager functionality.

## Usage

Run the Blazor application to view the Syncfusion File Manager component in the browser. The File Manager allows browsing and managing files through a web‑based interface.

## Configuration

Configuration for the File Manager component is handled within the Blazor application. Additional customization can be applied based on application requirements.

## Project Structure

`appsettings.json` - Application configuration and connection strings.

`Pages/Index.razor` - Main sample page implementing role-based File Manager authorization and toolbar customization.

`Controllers/HomeController.cs` - Contains both `HomeController` and `FileManagerController`, including file operation, upload, download, and image-processing endpoints.

`Areas/Identity/` - ASP.NET Core Identity UI assets and authentication pages.

`Data/ApplicationDbContext.cs` - Identity database context. [VERIFY: exact file path]

`Models/` - Supporting model classes used by the application.

`wwwroot/Files/` - Root directory used by the File Manager for file storage and management.

## Support and Feedback

- For general product questions, visit the [Syncfusion Community Forum](https://www.syncfusion.com/forums) or [Syncfusion Support](https://www.syncfusion.com/support).
- To report an issue specific to this sample, open a GitHub issue in this repository.
- Official File Manager documentation: https://help.syncfusion.com/file-manager-sdk/blazor/getting-started-with-web-app

## License

This is a Syncfusion sample project provided to demonstrate product usage. Review the [Syncfusion license terms](https://www.syncfusion.com/sales/pricing?category=ui-components) before using Syncfusion components in your own applications.