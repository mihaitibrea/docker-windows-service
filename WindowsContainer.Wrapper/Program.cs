using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsContainer.Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var service = new MyService())
            {
                if (Environment.UserInteractive)
                {
                    service.StartupAndStop(args);
                }
                else
                {
                    ServiceBase.Run(service);
                }
            }
        }
    }
}
