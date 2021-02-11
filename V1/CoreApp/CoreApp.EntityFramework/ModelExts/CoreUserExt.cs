using CoreApp.Common.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static CoreApp.Common.Constants.DataConstants;

namespace CoreApp.EntityFramework.Models
{
    public partial class CoreUser
    {
        [NotMapped]
        public bool IsSuperAdmin { get; set; }
        [NotMapped]
        public double Rating { get; set; }
        [NotMapped]
        public bool IsRated { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public bool IsValidImport { get; set; }
    }
}
