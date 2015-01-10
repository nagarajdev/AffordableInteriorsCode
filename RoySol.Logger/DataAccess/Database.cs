
namespace RoySol.Logger
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public static class DBLog
    {
        public static List<LogInfo> GetLogForLog4Net(DateTime startDate, DateTime endDate, string errorType, string searchText)
        {
            string constring = string.Empty;
            if (ConfigurationManager.AppSettings["Log4NetConnectionStringName"] != null && ConfigurationManager.ConnectionStrings["logging"] != null)
            {
                constring = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Log4NetConnectionStringName"].ToString()].ToString();
            }
            SqlDataReader rdr = null;
            // List<LogFileInfo> logFileInfo = new List<LogFileInfo>();
            List<LogInfo> listlogInfo = new List<LogInfo>();
            using (var con = new SqlConnection(constring))
            using (var cmd = con.CreateCommand())
            {
                try
                {
                    con.Open();
                }
                catch (InvalidOperationException)
                {
                    throw new Exception("add key[Log4NetConnectionStringName] in to  /app_config/Appsetting.config file");
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetDBLogForLog4Net";
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@errorType", errorType);
                cmd.Parameters.AddWithValue("@searchText", searchText);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listlogInfo.Add(new LogInfo()
                    {
                        datetimeoftheMessage = rdr["Date"] != null ? rdr["Date"].ToString() : string.Empty,
                        errorType = EnumUtil.ParseEnum<ERRORTYPE>(rdr["Level"] != null ? rdr["Level"].ToString() : string.Empty),
                        outMessage = rdr["Message"] != null ? rdr["Message"].ToString() : string.Empty,
                        Exception = rdr["Exception"] != null ? rdr["Exception"].ToString() : string.Empty,
                        Server = rdr["Server"] != null ? rdr["Server"].ToString() : string.Empty,
                        Market = rdr["Market"] != null ? rdr["Market"].ToString() : string.Empty,
                        Domain = rdr["Domain"] != null ? rdr["Domain"].ToString() : string.Empty,
                        RawUrl = rdr["RawUrl"] != null ? rdr["RawUrl"].ToString() : string.Empty,
                        IPAddress = rdr["IPAddress"] != null ? rdr["IPAddress"].ToString() : string.Empty,
                        Database = rdr["Database"] != null ? rdr["Database"].ToString() : string.Empty,
                        ItemId = rdr["ItemId"] != null ? rdr["ItemId"].ToString() : string.Empty
                    });
                }
            }
            return listlogInfo;
        }
        public static List<LogInfo> GetLogForEnterpriseLib(DateTime startDate, DateTime endDate, string errorType, string searchText)
        {
            string constring = ((System.Configuration.ConnectionStringsSection)(ConfigurationManager.GetSection("connectionStrings"))).ConnectionStrings[3].ToString();
            SqlDataReader rdr = null;
            // List<LogFileInfo> logFileInfo = new List<LogFileInfo>();
            List<LogInfo> listlogInfo = new List<LogInfo>();
            using (var con = new SqlConnection(constring))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetDBLogForEL";
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@errorType", errorType);
                cmd.Parameters.AddWithValue("@searchText", searchText);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var info = new LogInfo();
                    var date = string.Empty;
                    if (rdr["Timestamp"] != null)
                        info.datetimeoftheMessage = rdr["Timestamp"].ToString();
                    info.errorType = EnumUtil.ParseEnum<ERRORTYPE>(rdr["Severity"].ToString());
                    info.outMessage = rdr["Message"].ToString();
                    listlogInfo.Add(info);
                }
            }
            return listlogInfo;
        }
    }
}
