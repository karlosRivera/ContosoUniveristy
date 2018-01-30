using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;
namespace Contoso.Web.Log
{
    public class LoggingService : ILoggingService
    {
        private static readonly ILog log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);
        public void Log(string message)
        {
            log.Info(message);
        }
    }
    public interface ILoggingService
    {
        void Log(string message);
    }
}