namespace CoreApp.Common.Models
{
    public class ImportInputInfo<T> where T : class
    {
        public string FilePath { get; set; }
        public T DataImport { get; set; }
    }
}
