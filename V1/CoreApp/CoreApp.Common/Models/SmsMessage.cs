using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Common.Models
{
    public class SmsMessage
    {
        public string Body { get; set; }
        public string Originator { get; set; }
        public string[] Recipients { get; set; }
    }
}
