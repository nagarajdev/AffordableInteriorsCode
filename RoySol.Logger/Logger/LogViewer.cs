using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace RoySol.Logger
{
    public class SitecoreLogViewer
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsReadFromDB { get; set; }

        public List<LogFileInfo> ReadDBLog4Net(DateTime startDate, DateTime endDate, string errorType, string searchText)
        {
            List<LogFileInfo> listLogs = new List<LogFileInfo>();
            LogFileInfo logFileInfo = new LogFileInfo();
            logFileInfo.logInfo = DBLog.GetLogForLog4Net(startDate, endDate, errorType, searchText);
            listLogs.Add(logFileInfo);
            return listLogs;
        }

        public List<LogFileInfo> ReadDBLogForEnterpriseLib(DateTime startDate, DateTime endDate, string errorType, string searchText)
        {
            List<LogFileInfo> listLogs = new List<LogFileInfo>();
            LogFileInfo logFileInfo = new LogFileInfo();
            logFileInfo.logInfo = DBLog.GetLogForEnterpriseLib(startDate, endDate, errorType, searchText);
            listLogs.Add(logFileInfo);
            return listLogs;
        }

        public List<LogFileInfo> ReadLogFiles()
        {
            int counter = 0;
            string line;
            LogFileInfo objLogFileInfo;
            LogInfo objLogInfo = null;
            List<LogInfo> objLogInfolist;
            List<LogFileInfo> objListOfFiles = new List<LogFileInfo>();
            string LogFolderName = System.Configuration.ConfigurationManager.AppSettings["LogFolderName"];
            string[] filesPath = Directory.GetFiles(LogFolderName);
            List<string> files = new List<string>();
            foreach (string path in filesPath)
            {
                objLogFileInfo = new LogFileInfo();
                objLogFileInfo.fileName = Path.GetFileName(path);
                objLogFileInfo.folderName = Path.GetDirectoryName(path);
                int year = 0;
                int month = 0;
                int day = 0;
                string date = string.Empty;
                try
                {
                    date = objLogFileInfo.fileName.Substring(objLogFileInfo.fileName.IndexOf('[') + 1, 8);
                    int.TryParse(date.Substring(0, 4), out year);
                    int.TryParse(date.Substring(4, 2), out month);
                    int.TryParse(date.Substring(6, 2), out day);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid Log File Name, File name format should be in filename[date].txt... ex: RoySol.custom.log.20140718.txt");
                }
                if (!string.IsNullOrEmpty(date))
                {
                    objLogFileInfo.LoggedDate = new DateTime(year, month, day);
                    objLogInfolist = new List<LogInfo>();

                    using (System.IO.StreamReader file = new System.IO.StreamReader(path))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Length > 0)
                            {
                                if (line.StartsWith("Exception:"))
                                {
                                    objLogInfo = new LogInfo { datetimeoftheMessage = objLogFileInfo.LoggedDate.ToShortDateString(), outMessage = line.Substring(line.IndexOf("Exception:"), line.Length - 1) };
                                }
                                else
                                {
                                    objLogInfo = new LogInfo { datetimeoftheMessage = objLogFileInfo.LoggedDate.ToShortDateString(), outMessage = line.Substring(0, line.Length - 1) };
                                }
                            }
                            objLogInfolist.Add(objLogInfo);
                            counter++;
                        }
                        file.Close();
                        counter = 0;
                    }
                    objLogFileInfo.logInfo = (objLogInfolist);
                    objListOfFiles.Add(objLogFileInfo);
                }
            }

            return objListOfFiles;
        }


        public List<LogFileInfo> SearchLogFileInfo(List<LogFileInfo> logfileinfo)
        {
            List<LogFileInfo> LogSearchResult = new List<LogFileInfo>();
            LogFileInfo objLogfile;
            List<LogInfo> objlog = new List<LogInfo>();

            foreach (var logobj in logfileinfo)
            {
                logobj.logInfo.RemoveAll(item => item == null);

                var Result = from s in logobj.logInfo
                             where logobj.TypeofError.Contains(s.errorType)
                             select new { DateTimemessage = s.datetimeoftheMessage, ErrorType = s.errorType, Outmessage = s.outMessage, loggedDate = logobj.LoggedDate };

                foreach (var item in Result)
                {
                    LogInfo logInfo = new LogInfo();
                    logInfo.datetimeoftheMessage = item.loggedDate.ToShortDateString() + " " + item.DateTimemessage;
                    logInfo.errorType = item.ErrorType;
                    logInfo.outMessage = item.Outmessage;
                    objlog.Add(logInfo);
                }
                objLogfile = new LogFileInfo();
                objLogfile.logInfo = objlog;
                LogSearchResult.Add(objLogfile);
            }

            return LogSearchResult;
        }

        public List<LogFileInfo> SearchLogFileInfo(List<LogFileInfo> logfileinfo, string starDate, string endDate)
        {
            List<LogFileInfo> LogSearchResult = new List<LogFileInfo>();
            LogFileInfo objLogfile;
            List<LogInfo> objlog = new List<LogInfo>();

            foreach (var logobj in logfileinfo)
            {
                logobj.logInfo.RemoveAll(item => item == null);

                var Result = from s in logobj.logInfo
                             where ((logobj.TypeofError.Count() > 0 && logobj.TypeofError.Contains(s.errorType) &&
                             (logobj.LoggedDate >= Convert.ToDateTime(starDate) && logobj.LoggedDate <= Convert.ToDateTime(endDate))) ||
                             (logobj.TypeofError.Count() == 0 &&
                             (logobj.LoggedDate >= Convert.ToDateTime(starDate) && logobj.LoggedDate <= Convert.ToDateTime(endDate))))
                             select new { DateTimemessage = s.datetimeoftheMessage, ErrorType = s.errorType, Outmessage = s.outMessage, loggedDate = logobj.LoggedDate };

                foreach (var item in Result)
                {
                    LogInfo logInfo = new LogInfo();
                    logInfo.datetimeoftheMessage = item.loggedDate.ToShortDateString() + " " + item.DateTimemessage;
                    logInfo.errorType = item.ErrorType;
                    logInfo.outMessage = item.Outmessage;
                    objlog.Add(logInfo);
                }

            }
            objLogfile = new LogFileInfo();
            objLogfile.logInfo = objlog;
            LogSearchResult.Add(objLogfile);

            return LogSearchResult;
        }

        public List<LogFileInfo> SearchLogFileInfo(List<LogFileInfo> logfileinfo, string searchText)
        {
            List<LogFileInfo> LogSearchResult = new List<LogFileInfo>();
            LogFileInfo objLogfile;
            List<LogInfo> objlog = new List<LogInfo>();

            foreach (var logobj in logfileinfo)
            {
                logobj.logInfo.RemoveAll(item => item == null);

                var Result = from s in logobj.logInfo
                             select new { DateTimemessage = s.datetimeoftheMessage, ErrorType = s.errorType, Outmessage = s.outMessage, loggedDate = logobj.LoggedDate };

                foreach (var item in Result)
                {
                    if (item.Outmessage.Contains(searchText))
                    {
                        LogInfo logInfo = new LogInfo();
                        logInfo.datetimeoftheMessage = item.loggedDate.ToShortDateString() + " " + item.DateTimemessage;
                        logInfo.errorType = item.ErrorType;
                        logInfo.outMessage = item.Outmessage;
                        objlog.Add(logInfo);
                    }
                }

            }
            objLogfile = new LogFileInfo();
            objLogfile.logInfo = objlog;
            LogSearchResult.Add(objLogfile);

            return LogSearchResult;
        }

        private ERRORTYPE GetErrorType(string _input)
        {
            ERRORTYPE _errorType = ERRORTYPE.UNKNOWN;
            switch (_input)
            {
                case "WARN": _errorType = ERRORTYPE.WARNING; break;
                case "DEBUG": _errorType = ERRORTYPE.DEBUGS; break;
                case "ERROR": _errorType = ERRORTYPE.ERROR; break;
                case "INFO": _errorType = ERRORTYPE.INFO; break;
                case "FATALS": _errorType = ERRORTYPE.FATALS; break;
            }
            return _errorType;

        }
    }
}
