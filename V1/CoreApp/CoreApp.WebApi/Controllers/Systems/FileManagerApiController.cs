using CoreApp.Authentication.Jwt;
using CoreApp.Common.Constants;
using CoreApp.Common.Helpers;
using CoreApp.Common.Models;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class FileManagerApiController : NormalApiControllerBase
    {
        #region Fields
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserManager _userManager;
        #endregion

        #region Contructors

        public FileManagerApiController(ILogger<FileManagerApiController> logger, IUserManager userManager, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<FileUploadResultInfo> UploadTempFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileResultInfo = new FileUploadResultInfo();
            var extension = Path.GetExtension(file.FileName);
            var path = FileHelper.GetTempFilePath(extension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            fileResultInfo.FileName = Path.GetFileName(path);
            fileResultInfo.FilePath = path;

            return fileResultInfo;
        }

        [HttpPost]
        public async Task<FileUploadResultInfo> UploadFileManager(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;
            // Get root upload
            Request.Headers.TryGetValue("UploadRootPath", out var uploadRootPath);
            var host = Request.Host.Host;
            var scheme = Request.Scheme;
            var port = Request.Host.Port;
            var currentDate = DateTime.Now;

            var root = uploadRootPath.FirstOrDefault() ?? Path.Combine(FileConstants.FileManagerRootPath, currentDate.Year.ToString(), currentDate.Month.ToString(), currentDate.Day.ToString());
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var fileResultInfo = new FileUploadResultInfo();
            var fileExt = Path.GetExtension(file.FileName);
            var newFileName = FileHelper.GetRandomFileName(fileExt);
            var path = Path.Combine(root, newFileName);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Convert doc To Image
            if (fileExt.ToLower() == ".doc" || fileExt.ToLower() == ".docx")
            {
                FileHelper.ConvertDocToImage(root, newFileName);
            }

            // Convert pdf To Image
            if (fileExt.ToLower() == ".pdf")
            {
                var result = FileHelper.ConvertPdfToImage(root, newFileName);
                if (result.Count > 0)
                {
                    result.ToList().ForEach(item =>
                    {
                        FileHelper.GetThumbnailImage(root, item.FileName, 3);
                        item.FilePath = $"{scheme}://{host}/{ item.FilePath.Replace("wwwroot/", "")}";
                    });
                    return result.FirstOrDefault();
                }
            }
            fileResultInfo.FileName = newFileName;
            //Dev
            //fileResultInfo.FilePath = $"{scheme}://{host}:{port}/{ path.Replace("wwwroot/", "")}";
            //Product
            fileResultInfo.FilePath = $"{scheme}://{host}/{ path.Replace("wwwroot/", "")}";

            return fileResultInfo;
        }

        [HttpGet]
        public async Task<FileStreamResult> Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, FileHelper.GetContentType(path), Path.GetFileName(path));
        }

        [HttpGet]
        public FileManager GetFilesFromDirectory(string directoryPath, int pageIndex)
        {
            var host = Request.Host.Host;
            var scheme = Request.Scheme;
            var port = Request.Host.Port;

            if (string.IsNullOrEmpty(directoryPath))
            {
                directoryPath = Path.Combine(FileConstants.FileManagerRootPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            var data = FileHelper.GetFileFromDirectory(directoryPath, pageIndex);
            data.Files.ToList().ForEach(file =>
            {
                file.WebsitePath = $"{scheme}://{host}/{ file.WebsitePath}";
            });

            var homePath = Path.Combine(FileConstants.FileManagerRootPath);
            data.Breadcrumbs.Add(new Breadcrumb { Title = "Home", Path = homePath });
            var remainPath = directoryPath.Substring(homePath.Length);
            if (remainPath.Length > 0)
            {
                var remainDirectories = remainPath.Split("\\").Where(path => !string.IsNullOrEmpty(path)).ToArray();
                foreach (var name in remainDirectories)
                {
                    data.Breadcrumbs.Add(new Breadcrumb { Title = name, Path = Path.Combine(data.Breadcrumbs.Last().Path, name) });
                }
            }
            return data;
        }

        [HttpPost]
        public void CreateDirectory(CreateDirectoryModel model)
        {
            if (string.IsNullOrEmpty(model.DirectoryPath))
            {
                model.DirectoryPath = Path.Combine(FileConstants.FileManagerRootPath);
            }
            FileHelper.CreateDirectory(model.DirectoryPath, model.NewDirectoryName);
        }

        [HttpPost]
        public void DeleteFileOrDirectory(System.Collections.Generic.IEnumerable<string> arrPath)
        {
            //Array.ForEach(arrPath, path => FileHelper.DeleteFileOrDirectory(path));
            foreach (var path in arrPath)
            {
                FileHelper.DeleteFileOrDirectory(path);
            }
        }
        #endregion
    }
}
