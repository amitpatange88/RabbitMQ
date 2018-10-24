using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQ Rpc = new RabbitMQ();
            Rpc.CreateConnection();
            Console.ReadKey();
        }
    }
}
