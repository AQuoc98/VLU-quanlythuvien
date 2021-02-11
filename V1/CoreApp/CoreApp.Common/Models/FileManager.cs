using System.Collections.Generic;

namespace CoreApp.Common.Models
{
    public class FileManager
    {
        public IList<Breadcrumb> Breadcrumbs { get; set; }
        public IList<FileInfo> Files { get; set; }
        public IList<DirectoryInfo> Directories { get; set; }
        public int Total { get; set; }

        public FileManager()
        {
            Files = new List<FileInfo>();
            Directories = new List<DirectoryInfo>();
            Breadcrumbs = new List<Breadcrumb>();
        }
    }

    public class Breadcrumb
    {
        public string Path { get; set; }
        public string Title { get; set; }
    }
}
