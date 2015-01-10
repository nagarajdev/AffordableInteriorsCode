
namespace RoySol.Logger
{
    public class LogInfo
    {
        public string processId { get; set; } //[0-3] 4
        public string datetimeoftheMessage { get; set; } //[6-13] 
        public ERRORTYPE errorType { get; set; }//[15-17]4
        public string outMessage { get; set; } //[20-eol]     
        public string Exception { get; set; }
        public string Server { get; set; }
        public string Market { get; set; }
        public string Domain { get; set; }
        public string RawUrl { get; set; }
        public string IPAddress { get; set; }
        public string Database { get; set; }
        public string ItemId { get; set; }
    }
}
