using CoreApp.Common.Constants;
using CoreApp.Common.Models;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.EJ2.PdfViewer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace CoreApp.Common.Helpers
{
    public static class FileHelper
    {
        private static readonly string TmpFolderPath = "wwwroot/files/tmpfiles";
        private static readonly int PageSize = 12;

        /// <summary>
        /// Get content type
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Content type</returns>
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        /// <summary>
        /// Get temp file path
        /// </summary>
        /// <returns>File path</returns>
        public static string GetTempFilePath(string fileExtension)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TmpFolderPath, GetRandomFileName(fileExtension));
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string GetRandomFileName(string fileExtension)
        {
            return $"{Guid.NewGuid().ToString()}{fileExtension}";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath">Server path</param>
        /// <returns></returns>
        public static FileManager GetFileFromDirectory(string directoryPath, int pageIndex)
        {
            var result = new FileManager();

            // Get all file and directory
            var directoryInfo = new System.IO.DirectoryInfo(directoryPath);
            var allFilePath = directoryInfo.GetFiles().OrderByDescending(p => p.CreationTime).Select(s => Path.Combine(directoryPath, s.Name));
            var allDirectoyPath = Directory.GetDirectories(directoryPath);

            // Paging
            var allPaths = new List<PathInfo>();
            allPaths.AddRange(allDirectoyPath.Select(s => new PathInfo { Path = s, IsDirectory = true }));
            allPaths.AddRange(allFilePath.Select(s => new PathInfo { Path = s, IsFile = true }));

            result.Total = allPaths.Count();
            var skip = (pageIndex - 1) * PageSize;
            var allPathPaged = allPaths.Skip(skip).Take(PageSize).ToList();

            // Directories
            var allDirectoyPathPaged = allPathPaged.Where(p => p.IsDirectory).ToList();
            foreach (var pathItem in allDirectoyPathPaged)
            {
                var item = new Models.DirectoryInfo
                {
                    Name = GetDirectoryName(pathItem.Path),
                    Path = pathItem.Path
                };

                result.Directories.Add(item);
            }

            // Files
            var allFilePathPaged = allPathPaged.Where(p => p.IsFile).ToList();
            foreach (var pathItem in allFilePathPaged)
            {
                var item = new Models.FileInfo
                {
                    Name = Path.GetFileNameWithoutExtension(pathItem.Path),
                    WebsitePath = pathItem.Path.Replace("wwwroot", ""),
                    Path = pathItem.Path,
                    FileType = Path.GetExtension(pathItem.Path)
                };

                result.Files.Add(item);
            }

            // Breadcrumbs
            //var homePath = FileConstants.FileManagerRootPath;
            //result.Breadcrumbs.Add(new Breadcrumb { Title = "Home", Path = homePath });
            //var remainPath = directoryPath.Substring(homePath.Length);
            //if (remainPath.Length > 0)
            //{
            //    var remainDirectories = remainPath.Split("\\").Where(path => !string.IsNullOrEmpty(path)).ToArray();
            //    foreach (var name in remainDirectories)
            //    {
            //        result.Breadcrumbs.Add(new Breadcrumb { Title = name, Path = Path.Combine(result.Breadcrumbs.Last().Path, name) });
            //    }
            //}

            return result;
        }

        /// <summary>
        /// Create directory
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="newDirectoryName"></param>
        public static void CreateDirectory(string directoryPath, string newDirectoryName)
        {
            var path = Path.Combine(directoryPath, newDirectoryName);
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFileOrDirectory(string path)
        {
            var fi = new System.IO.FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
            else
            {
                var di = new System.IO.DirectoryInfo(path);


                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                di.Delete(true);
            }
        }

        public static void ConvertDocToImage(string startupPath, string fileName)
        {
            string inputFile = Path.Combine(Directory.GetCurrentDirectory(), startupPath, fileName);

            FileStream fileStream = new FileStream(inputFile, FileMode.Open);
            //Loads file stream into Word document
            WordDocument document = new WordDocument(fileStream, FormatType.Docx);
            fileStream.Dispose();
            //Instantiation of DocIORenderer for Word to PDF conversion
            DocIORenderer render = new DocIORenderer();
            //Converts Word document into PDF document
            PdfDocument pdfDocument = render.ConvertToPDF(document);
            //Releases all resources used by the Word document and DocIO Renderer objects
            render.Dispose();
            document.Dispose();
            //Saves the PDF file
            MemoryStream outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);
            outputStream.Position = 0;
            //Closes the instance of PDF document object
            pdfDocument.Close();

            PdfRenderer pdfExportImage = new PdfRenderer();
            //Loads the PDF document 
            pdfExportImage.Load(outputStream, new Dictionary<string, string>());
            //Exports the PDF document pages into images
            Bitmap[] bitmapimage = pdfExportImage.ExportAsImage(0, pdfExportImage.PageCount - 1);
            for (int i = 0; i < pdfExportImage.PageCount; i++)
            {
                bitmapimage[i].Save(Path.Combine(Directory.GetCurrentDirectory(), startupPath, string.Format("{1}_page_{0}", i, fileName.Split('.')[0]) + ".png"));
            }
        }

        public static IList<FileUploadResultInfo> ConvertPdfToImage(string startupPath, string fileName)
        {
            string inputFile = Path.Combine(Directory.GetCurrentDirectory(), startupPath, fileName);

            FileStream fileStream = new FileStream(inputFile, FileMode.Open);

            PdfRenderer pdfExportImage = new PdfRenderer();
            //Loads the PDF document
            pdfExportImage.Load(fileStream, new Dictionary<string, string>());
            //Exports the PDF document pages into images
            Bitmap[] bitmapimage = pdfExportImage.ExportAsImage(0, pdfExportImage.PageCount - 1);
            var listResult = new List<FileUploadResultInfo>();
            for (int i = 0; i < pdfExportImage.PageCount; i++)
            {
                var fileNameImage = string.Format("{1}_page_{0}", i, fileName.Split('.')[0]) + ".jpg";
                var pathImage = Path.Combine(Directory.GetCurrentDirectory(), startupPath, fileNameImage);
                bitmapimage[i].Save(pathImage);
                listResult.Add(new FileUploadResultInfo() { FileName = fileNameImage, FilePath = Path.Combine(startupPath, fileNameImage) });
            }
            return listResult;
        }

        public static void GetThumbnailImage(string path, string fileName, int CountResize)
        {
            string inputFile = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            FileStream fileStream = new FileStream(inputFile, FileMode.Open);

            Image image = Image.FromStream(fileStream);
            Image thumb = new Bitmap(image, new Size(image.Width / CountResize, image.Height / CountResize));
            thumb.Save(Path.ChangeExtension(inputFile, "thumb.jpg"),ImageFormat.Jpeg);
        }

        /// <summary>
        /// Types
        /// </summary>
        /// <returns>Dictionary of Types</returns>
        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        /// <summary>
        /// Get last folder name
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetDirectoryName(string path)
        {
            var startIndex = path.LastIndexOf("\\") + 1;
            return path.Substring(startIndex);
        }
    }
}
