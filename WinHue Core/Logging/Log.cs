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

        static Logger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo(@"log4net.config"));
        }

        public static ILog Log => log;

    }
}
