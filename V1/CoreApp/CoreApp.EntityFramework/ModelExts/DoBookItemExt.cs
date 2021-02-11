using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoreApp.EntityFramework.Models
{
    public partial class DoBookItem
    {
        [NotMapped]
        public int ToTalCount { get; set; }
        [NotMapped]
        public int SuccessCount { get; set; }
        [NotMapped]
        public bool IsValidImport { get; set; }
        [NotMapped]
        public IList<DoBookItem> InvalidData { get; set; }
    }
}
