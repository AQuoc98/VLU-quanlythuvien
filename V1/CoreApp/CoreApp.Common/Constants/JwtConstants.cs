namespace CoreApp.Common.Constants
{
    public static class JwtConstants
    {
        public static class ClaimIdentifiers
        {
            public const string Id = "id", Permissions = "permissions", Roles = "roles", Modules = "modules", phone = "phone_number";
        }

        public static class Claims
        {
            public const string ApiAccess = "api_access";
        }

        public static class Authorizations
        {
            public const string ModuleBasedPolicyPrefix = "ModuleBased";
        }
    }
}
