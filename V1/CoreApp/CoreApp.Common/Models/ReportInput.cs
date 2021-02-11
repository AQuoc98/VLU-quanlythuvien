using System;

namespace CoreApp.Common.Models
{
    public class ReportInput
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string OrderName { get; set; }
        public string Email { get; set; }
        public Guid BookingWebId { get; set; }
    }
}
