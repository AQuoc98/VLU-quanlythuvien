using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Common.Models
{
    public class SignInModel
    {
        public string LoginTypeCode { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }

        public bool IsPersistent { get; set; }
    }
}
