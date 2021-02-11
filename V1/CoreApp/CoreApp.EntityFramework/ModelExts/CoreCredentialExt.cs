using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.EntityFramework.Models
{
    public partial class CoreCredential
    {
        [NotMapped]
        public string NewSecret { get; set; }
        //[NotMapped]
        //public string cNewSecret { get; set; }
    }
}
