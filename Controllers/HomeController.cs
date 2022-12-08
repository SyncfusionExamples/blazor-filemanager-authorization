using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.FileProviders;
using Syncfusion.Blazor.FileManager;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;

namespace Blazor_Authentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public PhysicalFileProvider operation;
        public string basePath;
        string root = "wwwroot\\Files";
        [Obsolete]
        public SampleDataController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.basePath = hostingEnvironment.ContentRootPath;
            this.operation = new PhysicalFileProvider();
            this.operation.RootFolder(this.basePath + this.root); // It denotes in which files and folders are available.
        }

        // Processing the File Manager operations.
        [Route("FileOperations")]
        public object? FileOperations([FromBody] Syncfusion.EJ2.FileManager.Base.FileManagerDirectoryContent args, string Role)
        {

            if (args.Action == "read")
                // Path - Current path; ShowHiddenItems - Boolean value to show/hide hidden items.
                return this.operation.ToCamelCase(this.operation.GetFiles(args.Path, args.ShowHiddenItems));
            else if (args.Action == "search")
                // Path - Current path where the search is performed; SearchString - String typed in the searchbox; CaseSensitive - Boolean value which specifies whether the search must be casesensitive
                return this.operation.ToCamelCase(this.operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive));
            else if (args.Action == "details")
                // Path - Current path where details of file/folder is requested; Name - Names of the requested folders
                return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names));

            if (Role == "admin")
            {
                if (args.Action == "create")
                    // Path - Current path where the folder is to be created; Name - Name of the new folder
                    return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
                else if (args.Action == "delete")
                    // Path - Current path where the folder to be deleted; Names - Name of the files to be deleted
                    return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
            }

            if (Role == "admin" || Role == "employee")
            {
                if (args.Action == "copy")
                    //  Path - Path from where the file was copied; TargetPath - Path where the file/folder is to be copied; RenameFiles - Files with same name in the copied location that is confirmed for renaming; TargetData - Data of the copied file
                    return this.operation.ToCamelCase(this.operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
                else if (args.Action == "move")
                    // Path - Path from where the file was cut; TargetPath - Path where the file/folder is to be moved; RenameFiles - Files with same name in the moved location that is confirmed for renaming; TargetData - Data of the moved file
                    return this.operation.ToCamelCase(this.operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
                else if (args.Action == "rename")
                    // Path - Current path of the renamed file; Name - Old file name; NewName - New file name
                    return this.operation.ToCamelCase(this.operation.Rename(args.Path, args.Name, args.NewName));
            }

            return null;
        }
    }
}
