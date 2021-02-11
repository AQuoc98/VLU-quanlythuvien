using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Common.Constants
{
    public static class FieldConstants
    {
        public static readonly string IdentityField = "Id";
        public static class AuditFields
        {
            public static readonly string CreatedDate = "CreatedDate";
            public static readonly string CreatedBy = "CreatedBy";
            public static readonly string UpdatedDate = "UpdatedDate";
            public static readonly string UpdatedBy = "UpdatedBy";
        }
    }
}
