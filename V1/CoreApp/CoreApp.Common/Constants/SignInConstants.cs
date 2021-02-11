using System;

namespace CoreApp.Common.Constants
{
    public struct SignInConstants
    {
        public struct LoginType
        {
            public const string Email = "EMAIL";
            public const string Phone = "PHONE";
        }

        public struct LoginTypeIds
        {
            public static Guid Email = new Guid("dd5fe78a-2ee4-4e8c-a66c-c05025908955");
            public static Guid Phone = new Guid("7f80cb1c-591c-4dc2-9fce-0d45380631eb");
        }
    }
}
