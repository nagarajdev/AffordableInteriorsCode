
namespace RoySol.Logger
{
    using System;
    using log4net;
    using log4net.Config;

    public class Log4NetLogger : ILogger
    {
        /// <summary>
        /// logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(Log4NetLogger));
        
        /// <summary>
        /// Log4NetLogger
        /// </summary>
        static Log4NetLogger()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Log the exception to the DB
        /// </summary>
        /// <param name="message"></param>
        /// <param name="category"></param>
        public void Log(string message, Enum category)
        {
            Log4NetLogger.customLogInfo(message, category, null);
        }

        /// <summary>
        /// Log the exception to the DB
        /// </summary>
        /// <param name="message"></param>
        /// <param name="category"></param>
        /// <param name="ex"></param>
        public void Log(string message, Enum category, Exception ex)
        {
            Log4NetLogger.customLogInfo(message, category, ex);
        }

        /// <summary>
        /// custom Log Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="category"></param>
        /// <param name="ex"></param>
        public static void customLogInfo(string message, Enum category, Exception ex)
        {
            if (category.Equals(ExceptionCategory.Info))
            {
                logger.Info(message, ex);
            }
            else if (category.Equals(ExceptionCategory.Debug))
            {
                logger.Debug(message, ex);
            }
            else if (category.Equals(ExceptionCategory.Fatal))
            {
                logger.Fatal(message, ex);
            }
            else if (category.Equals(ExceptionCategory.Error))
            {
                logger.Error(message, ex);
            }
            else if (category.Equals(ExceptionCategory.Warn))
            {
                logger.Warn(message, ex);
            }
        }

    }
}
