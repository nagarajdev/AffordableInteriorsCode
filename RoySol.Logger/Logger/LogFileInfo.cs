using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoySol.Logger
{
    public class LogFileInfo
    {
        public string fileName { get; set; }
        public string folderName { get; set; }
        public List<LogInfo> logInfo { get; set; }
        public List<ERRORTYPE> TypeofError { get; set; }
        public DateTime LoggedDate { get; set; }

    }
}
