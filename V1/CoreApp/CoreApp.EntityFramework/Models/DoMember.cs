using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoMember
    {
        public DoMember()
        {
           
            DoBookLendings = new HashSet<DoBookLending>();
            DoPunishments = new HashSet<DoPunishment>();
            DoReservationBooks = new HashSet<DoReservationBook>();
        }
        [Key]
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string MemberCode { get; set; }
        public string Image { get; set; }
        public bool Gender { get; set; }
        public Guid? MemberGroupId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual DoMemberGroup MemberGroup { get; set; }
        public virtual ICollection<DoBookLending> DoBookLendings { get; set; }
        public virtual ICollection<DoPunishment> DoPunishments { get; set; }
        public virtual ICollection<DoReservationBook> DoReservationBooks { get; set; }
    }
}