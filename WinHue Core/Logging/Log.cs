using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using log4net;

namespace WinHue_Core.Logging
{
    public static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());

        static Logger()
        {
           
            var fileInfo = new FileInfo(@"log4net.config");

            log4net.Config.XmlConfigurator.Configure(repository, fileInfo);
        }

        public static ILog Log => log;

    }
}
