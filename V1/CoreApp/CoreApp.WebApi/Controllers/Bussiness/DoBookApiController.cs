using CoreApp.Common.Models;

using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoBookApiController : ModuleApiControllerBase<DoBook>
    {
        #region Fields

        private readonly IBookService _service;
        private IHostingEnvironment hostingEnvironment;
        #endregion

        #region Contructors

        public DoBookApiController(ILogger<DoBookApiController> logger, IBookService service,
            IHostingEnvironment hostingEnvironment)
            : base(logger, service)
        {

            _service = service;
            this.hostingEnvironment = hostingEnvironment;
        }

        #endregion



        [HttpPost]
        public virtual ResultModel UploadBook(DoBook entity)
        {
            //SaveImage(entity);
            return _service.Insert(entity);
        }
        [HttpPost]
        public virtual ResultModel UpdateBook(DoBook entity)
        {

            //UpdateImage(entity);
            return _service.Update(entity);
        }
       
        #region Image
        [EnableCors("MyPolicy")]
        [HttpPost]
        public virtual object SaveImage([FromForm] string file)
        {
            var resultMessages = new List<string>();
            if (Request.Form.Files.Count == 0)
                return new object();

            string contentPath = this.hostingEnvironment.WebRootPath;



            var uploadedFiles = new List<string>();
            foreach (var postedFile in Request.Form.Files)
            {
                string[] extensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };
                if (!extensions.Any(x => x.Equals(Path.GetExtension(postedFile.FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
                {
                    throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
                }

                string fileName = Path.GetFileName(postedFile.FileName);

                file = fileName;
                string path = Path.Combine(contentPath + "/Uploads", file);


                using (FileStream stream = new FileStream(Path.Combine(path), FileMode.Create))
                {
                    postedFile.CopyTo(stream);

                    uploadedFiles.Add(file);
                    resultMessages.Add("Save Image Successfully");
                }
            }

            return new
            {

                Files = uploadedFiles
            };
        }
        #endregion

        [HttpPost]
        public async Task<string> UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, file.FileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            var host = Request.Host.Host;
            var scheme = Request.Scheme;
            var port = Request.Host.Port;
            var fileUrl = "";
            if (host == "localhost")
            {
                fileUrl = $"{scheme}://{host}:{port}/{ Path.Combine("Uploads", file.FileName)}";
            }
            else
            {
                fileUrl = $"{scheme}://{host}:{port}/{ Path.Combine("Uploads", file.FileName)}";
            }
            return fileUrl;
        }

        [HttpGet]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            if (Directory.Exists(uploads))
            {
                var provider = hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }

    }
}
