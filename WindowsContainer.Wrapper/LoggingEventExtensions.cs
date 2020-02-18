using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsContainer.Wrapper
{
    internal static class LoggingEventExtensions
    {
        internal static string GetXmlString(this LoggingEvent loggingEvent, ILayout layout = null)
        {
            string message = loggingEvent.RenderedMessage + Environment.NewLine + loggingEvent.GetExceptionString();
            if (layout != null)
            {
                using (var w = new StringWriter())
                {
                    layout.Format(w, loggingEvent);
                    message = w.ToString();
                }
            }

            return message;
        }
    }
}
