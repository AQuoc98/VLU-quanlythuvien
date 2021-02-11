using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
   public partial class DoPolicy
    {
        [Key]
        public Guid Id { get; set; }
        public int BookNumber { get; set; }
        public int numberOfDueDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? MemberGroupId { get; set; }
        public virtual DoMemberGroup MemberGroup { get; set; }
    }
}
