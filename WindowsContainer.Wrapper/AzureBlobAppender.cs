using log4net.Appender;
using log4net.Core;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsContainer.Wrapper
{
    public class AzureBlobAppender : BufferingAppenderSkeleton
    {
        private CloudStorageAccount _account;
        private CloudBlobClient _client;
        private CloudBlobContainer _cloudBlobContainer;

        public string ConnectionStringName { get; set; }
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
               
                if (String.IsNullOrEmpty(_connectionString))
                    throw new ApplicationException("Connection string not specified");
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        private string _containerName;

        public string ContainerName
        {
            get
            {
                if (String.IsNullOrEmpty(_containerName))
                    throw new ApplicationException("Container name not specified");
                return _containerName;
            }
            set
            {
                _containerName = value;
            }
        }

        private string _directoryName;

        public string DirectoryName
        {
            get
            {
                if (String.IsNullOrEmpty(_directoryName))
                    throw new ApplicationException("Directory name not specified");
                return _directoryName;
            }
            set
            {
                _directoryName = value;
            }
        }

        protected override void SendBuffer(LoggingEvent[] events)
        {
            Parallel.ForEach(events, ProcessEvent);
        }
        private void ProcessEvent(LoggingEvent loggingEvent)
        {
            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(Filename(loggingEvent, _directoryName));
            var xml = loggingEvent.GetXmlString(Layout);
            blob.UploadText(xml);
        }

        private static string Filename(LoggingEvent loggingEvent, string directoryName)
        {
            return string.Format("{0}/{1}.log",
                                 directoryName,
                                 loggingEvent.TimeStamp.ToString("yyyy_MM_dd_HH_mm_ss_fffffff", DateTimeFormatInfo.InvariantInfo));
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            base.BufferSize = 1;

            _account = CloudStorageAccount.Parse(ConnectionString);
            _client = _account.CreateCloudBlobClient();
            _cloudBlobContainer = _client.GetContainerReference(ContainerName.ToLower());
            _cloudBlobContainer.CreateIfNotExists();
        }
    }
}
