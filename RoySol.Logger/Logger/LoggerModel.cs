
namespace RoySol.Logger
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class LoggerModel
    {
        public string SearchText { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<LogFileInfo> LogFileInfoList { get; set; }

        public IEnumerable<LoggerLevel> AvailableLoggerLevels { get; set; }
        public IEnumerable<LoggerLevel> SelectedLoggerLevels { get; set; }

        public PostedLoggerLevels PostedLogLevels { get; set; }

        //dropdownlist
        public int SelectedLoggerId { get; set; }
        public IEnumerable<SelectListItem> LoggedItems
        {
            get;
            set;
        }
        public List<SitecoreLogViewer> GetDropdownListItems(string configPath)
        {
            try
            {
                List<SitecoreLogViewer> list = new List<SitecoreLogViewer>();
                XDocument doc = XDocument.Load(configPath);
                var nodelist = from node in doc.XPathSelectElements("Logger/LoggerTypes").Elements() select node;

                foreach (var item in nodelist)
                {
                    SitecoreLogViewer logvwr = new SitecoreLogViewer();
                    logvwr.Id = item.Attribute("value").Value;
                    logvwr.Name = item.Attribute("text").Value;
                    list.Add(logvwr);
                }
                //Insert the Selectitem
                list.Insert(0,new SitecoreLogViewer() { Id = "0", Name = "Select" });
                return list;
            }
            catch(XmlException ex)
            {
                throw ex;
            }
        }
    }
}
