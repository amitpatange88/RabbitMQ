using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class Program
    {
        public delegate void _CallbackConsumerDel(string message);
        public static _CallbackConsumerDel _callback;

        static void Main(string[] args)
        {
            Program p = new Program();
            _callback = new _CallbackConsumerDel(p.OnConsumeMessage);
            using(RabbitMQ Rpc = new RabbitMQ())
            {
                Rpc.MessageBrokerPublish("This is test message created using RabbitMQ message broker.");
                Rpc.MessageBrokerConsume(_callback);
            }

            Console.ReadKey();
        }

        void OnConsumeMessage(string message)
        {
            Console.WriteLine(" [x] Received {0}", message);
        }
    }
}
