using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.FileProviders;
using Syncfusion.Blazor.FileManager;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Syncfusion.Blazor.FileManager.Base;

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
    public class FileManagerController : Controller
    {
        public PhysicalFileProvider operation;
        public string basePath;
        string root = "wwwroot\\Files";

        public FileManagerController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            this.basePath = hostingEnvironment.ContentRootPath;
            this.operation = new PhysicalFileProvider();
            this.operation.RootFolder(this.basePath + this.root); // It denotes in which files and folders are available.
        }

        // Processing the File Manager operations.
        [Route("FileOperations")]
        public object? FileOperations([FromBody] FileManagerDirectoryContent args, string Role)
        {
            if (Role == "Admin")
            {
                if (args.Action == "create")
                    // Path - Current path where the folder is to be created; Name - Name of the new folder
                    return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
                else if (args.Action == "delete")
                    // Path - Current path where the folder to be deleted; Names - Name of the files to be deleted
                    return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
            }

            if (Role == "Admin" || Role == "Employee")
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

            if (args.Action == "read")
                // Path - Current path; ShowHiddenItems - Boolean value to show/hide hidden items.
                return this.operation.ToCamelCase(this.operation.GetFiles(args.Path, args.ShowHiddenItems));
            else if (args.Action == "search")
                // Path - Current path where the search is performed; SearchString - String typed in the searchbox; CaseSensitive - Boolean value which specifies whether the search must be casesensitive
                return this.operation.ToCamelCase(this.operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive));
            else if (args.Action == "details")
                // Path - Current path where details of file/folder is requested; Name - Names of the requested folders
                return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names));

            return null;
        }

        [Route("Upload")]
        public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
        {
            FileManagerResponse uploadResponse;
            foreach (var file in uploadFiles)
            {
                var folders = (file.FileName).Split('/');
                // checking the folder upload
                if (folders.Length > 1)
                {
                    for (var i = 0; i < folders.Length - 1; i++)
                    {
                        string newDirectoryPath = Path.Combine(this.basePath + path, folders[i]);
                        if (!Directory.Exists(newDirectoryPath))
                        {
                            this.operation.ToCamelCase(this.operation.Create(path, folders[i]));
                        }
                        path += folders[i] + "/";
                    }
                }
            }
            uploadResponse = operation.Upload(path, uploadFiles, action, null);
            if (uploadResponse.Error != null)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
            }
            return Content("");
        }
        /// <summary>
        /// downloads the selected file(s) and folder(s)
        /// </summary>
        /// <param name="downloadInput"></param>
        /// <returns></returns>
        //[HttpPost]
        [Route("Download")]
        public IActionResult Download(string downloadInput)
        {
            FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
            return operation.Download(args.Path, args.Names, args.Data);
        }

        /// <summary>
        ///  gets the image(s) from the given path
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        //[HttpGet]
        [Route("GetImage")]
        public IActionResult GetImage(FileManagerDirectoryContent args)
        {
            return this.operation.GetImage(args.Path, args.Id, false, null, null);
        }

    }
}
