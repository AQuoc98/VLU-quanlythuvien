using System.Collections.Generic;
using System.Data.SqlClient;

namespace CoreApp.Logger.Helpers
{
    public class SqlHelper
    {

        #region Fields

        #endregion

        #region Contructors

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="connectionStr"></param>
        public SqlHelper(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private bool ExecuteNonQuery(string commandStr, List<SqlParameter> paramList)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand command = new SqlCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    int count = command.ExecuteNonQuery();
                    result = count > 0;
                }
            }
            return result;
        }

        /// <summary>
        /// Insert Log 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool InsertLog(Models.EventLog log)
        {
            string command = $@"INSERT INTO [dbo].[CoreEventLogs] ([EventId], [Logger],[LogLevel],[Message],[CreatedDate]) VALUES (@EventId, @Logger, @LogLevel, @Message, @CreatedDate)";
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("EventId", log.EventId));
            paramList.Add(new SqlParameter("Logger", log.Logger));
            paramList.Add(new SqlParameter("LogLevel", log.LogLevel));
            paramList.Add(new SqlParameter("Message", log.Message));
            paramList.Add(new SqlParameter("CreatedDate", log.CreatedDate));
            return ExecuteNonQuery(command, paramList);
        }

        #endregion

        #region Properties

        private string ConnectionString { get; set; }

        #endregion
    }
}
