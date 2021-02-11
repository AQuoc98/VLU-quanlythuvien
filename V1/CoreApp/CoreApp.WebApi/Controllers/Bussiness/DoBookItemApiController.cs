using CoreApp.Common.Enums;
using CoreApp.Common.Helpers;
using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using CoreApp.Repository;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoBookItemApiController : ModuleApiControllerBase<DoBookItem>
    {
        #region Fields
        private readonly IRepository<DoBookItem> _bookrep;
        private CoreAppDbContext db;
        //private static readonly string TmpFolderPath = "wwwroot/files/TmpFiles/Book1.xlsx";
        private IBookitemService _service;
        private IHostingEnvironment hostingEnvironment;
        private IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Contructors

        public DoBookItemApiController(CoreAppDbContext db, ILogger<DoBookItemApiController> logger, IBookitemService service,
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IRepository<DoBookItem> bookrep)
            : base(logger, service)
        {
            this.db = db;
            this._bookrep = bookrep;
            this._service = service;
            this.hostingEnvironment = hostingEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Method
        [HttpPost]
        public async Task<string> UploadTmpFile(IFormFile file)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "TmpFolder");
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

            return filePath;
        }
        #endregion
        #region
        [HttpPost]
        public ResultModel<DoBookItem> StartImport(IFormFile file)
        {

            var resultModel = new ResultModel<DoBookItem>();
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "TmpFolder");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, file.FileName);
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            string relative_path = filePath;
            string absolute_path = Path.GetFullPath(relative_path);

            var fi = new System.IO.FileInfo(absolute_path);
            var data = ExcelHelper.ReadData(fi);

            var book = BookItemImportConverter(data);
            var bookInserts = book.Where(x => x.IsValidImport == true).ToList();
            var countSaved = _service.Insert(bookInserts);
            resultModel.ExtendData = new DoBookItem()
            {
                ToTalCount = book.Count,
                SuccessCount = bookInserts.Count,
                InvalidData = book.Where(x => x.IsValidImport == false).ToList()

            };

            var resultToast = $" <br/>Thành công: {resultModel.ExtendData.SuccessCount} | Thất bại: {resultModel.ExtendData.ToTalCount - resultModel.ExtendData.SuccessCount}";
            if (countSaved == 0)
            {
                // Fail
                resultModel.Messages = new string[] { $"Import thất bại. {resultToast}" };
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            resultModel.Messages = new string[] { $"Import thành công. {resultToast}" };
            resultModel.Status = ResultStatus.Success;

            ////delet file when imported successs
            if (fi.Exists)
            {
                fi.Delete();
            }
            else
            {
                var di = new System.IO.DirectoryInfo(absolute_path);
                foreach (System.IO.FileInfo files in di.GetFiles())
                {
                    files.Delete();
                }
            }



            return resultModel;


        }
        #endregion
        #region Import

        private IList<DoBookItem> BookItemImportConverter(DataTable data)
        {
            var Request = httpContextAccessor.HttpContext.Request;
            var result = new List<DoBookItem>();

            var barCodes = new List<string>();
            foreach (DataRow row in data.Rows) barCodes.Add(row[0].ToString());
            var barCodesExit = _service.GetByBarCode(barCodes.ToArray()).Select(x => x.Barcode).ToArray();

            foreach (DataRow row in data.Rows)
            {
                var entity = new DoBookItem();
                try
                {
                    var checkExit = barCodesExit.Contains(row[0].ToString());
                    if (checkExit)
                    {
                        entity.IsValidImport = false;
                        result.Add(new DoBookItem()
                        {
                            Barcode = row[0].ToString(),
                            Price = int.Parse(row[1].ToString()),
                            PublicationYear = row[2].ToString(),
                            Status = new DoStatus() { Name = row[3].ToString() },
                            Rack = new DoRack() { Number = row[4].ToString() },
                            Format = new DoFormat() { Name = row[5].ToString() },
                            Book = new DoBook() { ISBN = row[6].ToString() },
                            IsRareBook = Convert.ToBoolean(row[7].ToString()),
                            IsReferenceOnly = Convert.ToBoolean(row[8].ToString())
                        });
                        continue;
                    }
                    //Barcode
                    var barcode = row[0].ToString();
                    entity.Barcode = barcode;

                    //Price 
                    var price = row[1].ToString();
                    entity.Price = int.Parse($"{price}");
                    //Publication
                    var publication = row[2].ToString();
                    entity.PublicationYear = ($"{publication}");


                    //Status    
                    var statusName = row[3].ToString();
                    entity.Status = db.DoStatus.SingleOrDefault(s => s.Name.Equals(statusName, StringComparison.OrdinalIgnoreCase));
                    entity.StatusId = entity.Status?.Id;
                    //RackId  
                    var rackNumber = row[4].ToString();
                    entity.Rack = db.DoRacks.SingleOrDefault(s => s.Number.Equals(rackNumber, StringComparison.OrdinalIgnoreCase));
                    entity.RackId = entity.Rack?.Id;
                    //FormatId  
                    var formatName = row[5].ToString();
                    entity.Format = db.DoFormats.SingleOrDefault(s => s.Name.Equals(formatName, StringComparison.OrdinalIgnoreCase));
                    entity.FormatId = entity.Format?.Id;
                    //BookId    
                    var bookISBN = row[6].ToString();
                    entity.Book = db.DoBooks.SingleOrDefault(s => s.ISBN.Equals(bookISBN, StringComparison.OrdinalIgnoreCase));
                    entity.BookId = entity.Book?.Id;

                    //IsRareBook
                    var rare = row[7].ToString();
                    entity.IsRareBook = Convert.ToBoolean(rare);
                    //IsReferenceOnly
                    var refer = row[8].ToString();
                    entity.IsReferenceOnly = Convert.ToBoolean(refer);
                    if (entity.StatusId == null || entity.FormatId == null || entity.RackId == null || entity.BookId == null)
                    {
                        entity.IsValidImport = false;
                    }
                    else
                    {
                        entity.IsValidImport = true;
                    }

                }
                catch (Exception e)
                {
                    entity.IsValidImport = false;
                    result.Add(new DoBookItem()
                    {
                        Barcode = row[0].ToString(),
                        Price = int.Parse(row[1].ToString()),
                        PublicationYear = row[2].ToString(),
                        Status = new DoStatus() { Name = row[3].ToString() },
                        Rack = new DoRack() { Number = row[4].ToString() },
                        Format = new DoFormat() { Name = row[5].ToString() },
                        Book = new DoBook() { ISBN = row[6].ToString() },
                        IsRareBook = Convert.ToBoolean(row[7].ToString()),
                        IsReferenceOnly = Convert.ToBoolean(row[8].ToString())
                    });
                    continue;

                }
                result.Add(entity);
            }

            return result;
        }
        #endregion
        #region Export
        [HttpGet]
        public string Download()
        {
            var resultModel = new ResultModel<DoBookItem>();
            string rootFolder = Path.Combine(hostingEnvironment.WebRootPath, "Download");
            if (!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }
            string fileName = @"BookItem.xlsx";
            //System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            var file = new FileStream(Path.Combine(rootFolder, fileName), FileMode.OpenOrCreate);

            using (ExcelPackage package = new ExcelPackage())
            {
                IList<DoBookItem> bookList = _service.GetAll().ToList();   /*db.DoBookItems.ToList();*/
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("BookItem");
                int totalRows = bookList.Count();

                worksheet.Cells[1, 1].Value = "Barcode";
                worksheet.Cells[1, 2].Value = "Price";
                worksheet.Cells[1, 3].Value = "Publication Year";
                worksheet.Cells[1, 4].Value = "Rack Number";
                worksheet.Cells[1, 5].Value = "Format Name";
                worksheet.Cells[1, 6].Value = "Book ISBN";
                worksheet.Cells[1, 7].Value = "Status";
                worksheet.Cells[1, 8].Value = "Is Rare Book";
                worksheet.Cells[1, 9].Value = "Is Reference Book";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = bookList[i].Barcode;
                    worksheet.Cells[row, 2].Value = bookList[i].Price;
                    worksheet.Cells[row, 3].Value = bookList[i].PublicationYear;
                    //Rack 
                    worksheet.Cells[row, 4].Value = bookList[i].Rack.Number;
                    //Format

                    worksheet.Cells[row, 5].Value = bookList[i].Format.Name;
                    //Book
                    worksheet.Cells[row, 6].Value = bookList[i].Book.ISBN;
                    //Status
                    worksheet.Cells[row, 7].Value = bookList[i].Status.Name;
                    //Rare
                    worksheet.Cells[row, 8].Value = bookList[i].IsRareBook;
                    //Reference
                    worksheet.Cells[row, 9].Value = bookList[i].IsReferenceOnly;
                    i++;
                }
                package.SaveAs(file);
                file.Close();

            }
            var host = Request.Host.Host;
            var scheme = Request.Scheme;
            var port = Request.Host.Port;
            var fileUrl = "";
            if (host == "localhost")
            {
                fileUrl = $"{scheme}://{host}:{port}/{ Path.Combine("Download", fileName)}";
            }
            else
            {
                fileUrl = $"{scheme}://{host}:{port}/{ Path.Combine("Download", fileName)}";
            }
            return fileUrl;
        }
        #endregion
    }

}
