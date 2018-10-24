using System;

namespace RabbitMQ
{
    public class SystemDetails
    {
        public Guid LogId { get; set; }

        public DateTime PreciseTimeStamp { get; set; }

        public DateTime UTCTimeStamp { get; set; }

        public string MachineName { get; set; }

        public bool Is64BitOperatingSystem { get; set; }

        public string OSVersion { get; set; }

        public int ProcessorCount { get; set; }

        public int OnCount { get; set; }

        public string ProcessesRunning { get; set; }
    }
}
