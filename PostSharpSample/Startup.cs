using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpSample
{
    public class Startup
    {
        public static void Init()
        {
            var backend = new PostSharp.Patterns.Diagnostics.Backends.Console.ConsoleLoggingBackend();
            backend.Options.Delimiter = "\t";
            backend.Options.IncludeTimestamp = true;
            backend.Options.TimestampFormat = "yyyy-MM-dd HH:mm:ss,fff";

            LoggingServices.DefaultBackend = backend;

            //AuditServices.RecordPublished += AuditServices_RecordPublished;
        }
        private static void AuditServices_RecordPublished(object sender, AuditRecordEventArgs e)
        {
            Logger.GetLogger().Write(LogLevel.Trace, "AuditServices_RecordPublished:" + e.Record.Text);
        }
    }
}
