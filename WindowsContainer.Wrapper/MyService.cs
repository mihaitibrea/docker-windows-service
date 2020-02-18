using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using System.Runtime.InteropServices;

namespace WindowsContainer.Wrapper
{
    partial class MyService : ServiceBase
    {
        private readonly Timer _timer;
        private const int _timerIntervalMs = 5000;
        private readonly ILog _logger;

        public MyService()
        {
            InitializeComponent();

            _timer = new Timer(_timerIntervalMs);
            _timer.Elapsed += DoStuff;
            _timer.Enabled = false;

            _logger = LogManager.GetLogger("MyService");
            log4net.Config.XmlConfigurator.Configure();            
        }


        internal void StartupAndStop(string[] args)
        {
            OnStart(args);

            Console.ReadLine();

            _logger.Info("Terminating the application.");
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
        }


        private void DoStuff(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Enabled = false;
                var message = $"Doing stuff at {DateTime.Now.ToLongTimeString()}";

                _logger.Info(message);
         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            finally
            {
                _timer.Enabled = true;
            }
        }
    }
}
