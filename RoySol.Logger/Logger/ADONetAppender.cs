

namespace RoySol.Logger
{
    #region "Using Namespace"

    using System;
    using System.Configuration;
    using System.Web;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Util;
    using Sitecore;
    using Sitecore.Web;
    #endregion

    /// <summary>
    /// Extending the functionality of AdonetAppender to add custom parameters to make logged error more readability.
    /// </summary>
    public class ADONetAppender : AdoNetAppender
    {
        /// <summary>
        /// connectionStringName attribute
        /// </summary>
        private string connectionStringName;

        /// <summary>
        /// ConnectionStringName attribute
        /// </summary>
        public string ConnectionStringName
        {
            get
            {
                return this.connectionStringName;
            }
            set
            {
                this.connectionStringName = value;
                if (!string.IsNullOrEmpty(this.connectionStringName))
                {
                    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[this.connectionStringName];
                    if (connectionString == null)
                    {
                        throw new LogException(string.Format("Could not find Connection String for [{0}]", this.connectionStringName));
                    }
                    this.ConnectionString = connectionString.ConnectionString; 
                }
            }
        }

        /// <summary>
        /// Overridden method to append custom properties to the AdoNetAppender class
        /// 
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            PropertiesDictionary properties = loggingEvent.Properties;

            properties["market"] = Context.Site != null ? Context.Site.Name : string.Empty;
            properties["database"] = Context.Database != null ? Context.Database.Name : string.Empty;
            properties["itemid"] = Context.Item != null ? Context.Item.ID.ToString() : string.Empty;
            properties["ipaddress"] = ADONetAppender.GetIPAddress();
            try
            {
                properties["server"] = Environment.MachineName;
            }
            catch (InvalidOperationException)
            {
                properties["server"] = string.Empty;
            }

            try
            {
                properties["domain"] = WebUtil.GetHostName();
            }
            catch (System.Web.HttpException)
            {

                properties["domain"] = string.Empty;
            }
            try
            {
                properties["rawurl"] = WebUtil.GetServerUrl() + WebUtil.GetRawUrl();
            }
            catch (System.Web.HttpException)
            {

                properties["rawurl"] = string.Empty;
            }

            base.Append(loggingEvent);
        }

        /// <summary>
        /// Summary:
        ///        Get Ip Address of current Request
        ///Exceptions:
        ///        System.InvalidOperationException:
        ///         The name of this computer cannot be obtained.
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            try
            {
                System.Web.HttpRequest currentRequest = System.Web.HttpContext.Current.Request;
                if (currentRequest != null)
                {
                    ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"] != null ? currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString() : string.Empty;
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"] != null ? currentRequest.ServerVariables["REMOTE_ADDR"].ToString() : string.Empty;
                    }
                }
            }
            catch (HttpException)
            {
                ipAddress = string.Empty;
            }

            return ipAddress;
        }
    }
}
