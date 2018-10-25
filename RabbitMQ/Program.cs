using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class Program
    {
        public delegate void _CallbackConsumerDel(string message);
        public static _CallbackConsumerDel _callback;
        private static int SystemContinuosOnCount = 1;

        static void Main(string[] args)
        {
            Program p = new Program();
            _callback = new _CallbackConsumerDel(p.OnConsumeMessage);
            SystemDetails s1 = CreateSystemObject();
            string systemLog = SerializeJSONData(s1);

            using (RabbitMQ Rpc = new RabbitMQ())
            {
                Rpc.MessageBrokerPublish(systemLog);
                Rpc.MessageBrokerPublish(systemLog);
                Rpc.MessageBrokerPublish(systemLog);
                //Rpc.MessageBrokerConsume(_callback);
            }

            Console.ReadKey();
        }

        void OnConsumeMessage(string message)
        {
            var details = DeserializeJSONData(message);
            Console.WriteLine(" [x] Received {0}", message);
        }

        public static SystemDetails CreateSystemObject()
        {
            SystemDetails s1 = new SystemDetails()
            {
                LogId = Guid.NewGuid(),
                PreciseTimeStamp = DateTime.Now,
                UTCTimeStamp = DateTime.UtcNow,
                MachineName = System.Environment.MachineName,
                OSVersion = System.Environment.OSVersion.ToString(),
                Is64BitOperatingSystem = System.Environment.Is64BitOperatingSystem,
                ProcessorCount = System.Environment.ProcessorCount,
                OnCount = SystemContinuosOnCount,
                ProcessesRunning = ProcessesRunningForLogs()
            };

            return s1;
        }

        /// <summary>
        /// Serialize the JSON data. Using with Default contract resolver for non public fields.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static string SerializeJSONData(SystemDetails e)
        {
            Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();

            Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            jss.ContractResolver = dcr;

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(e, jss);

            return response;
        }

        private static SystemDetails DeserializeJSONData(string message)
        {
            Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();

            Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            jss.ContractResolver = dcr;

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<SystemDetails>(message);

            return response;
        }

        private static string ProcessesRunningForLogs()
        {
            string processesRunning = string.Empty;
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    processesRunning += p.MainWindowTitle + "&nbsp;";
                }
            }

            return processesRunning;
        }
    }
}
