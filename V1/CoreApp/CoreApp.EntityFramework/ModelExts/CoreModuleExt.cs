using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.EntityFramework.Models
{
    public partial class CoreModule
    {
        [NotMapped]
        public IList<CorePermission> Permissions { get; set; }
    }
}
