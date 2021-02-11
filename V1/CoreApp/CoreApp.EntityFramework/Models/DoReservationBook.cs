using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoReservationBook

    {
        //public DoReservationBook()
        //{
        //    DoMembers = new HashSet<DoMember>();

        //}
        [Key]
        public Guid Id { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ExpectedDay { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? MemberId { get; set; }
        public virtual DoMember Member { get; set; }
        public Guid? BookId { get; set; }
        public virtual DoBook Book { get; set; }
        public Guid? BookItemId { get; set; }
        public virtual DoBookItem BookItem { get; set; }

        //public virtual ICollection<DoMember> DoMembers { get; set; }



    }
}
