namespace CoreApp.Common.Constants
{
    public struct SqlConstant
    {
        //Access
        public struct DataAccess
        {
            public const string AccessConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|App_Data|DbContext.mdb;Persist Security Info=False;Jet OLEDB:Database Password=246357cn";
        }

        public struct DatabaseConnectionName
        {
            public const string MainDatabaseConnectionName = "MainDatabase";
            public const string LoggerDatabaseConnectionName = "LoggerDatabase";
        }
    }
}
