using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CoreApp.EntityFramework.Models
{
    public partial class DoMemberGroup
    {
        public DoMemberGroup()
        {
            DoMembers = new HashSet<DoMember>();
            DoPolicys = new HashSet<DoPolicy>();
        }
        [Key]
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<DoMember> DoMembers { get; set; }
        public virtual ICollection<DoPolicy> DoPolicys { get; set; }
    }
}
