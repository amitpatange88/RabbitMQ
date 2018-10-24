using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    internal class Consumer
    {
        internal void ConsumeMessage(IModel channel, Delegate callback)
        {
            try
            {
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    OnConsumeMessage(message);
                };

                channel.BasicConsume(queue: RabbitConstants.Queue,
                                        autoAck: true,
                                        consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
            }
            catch(Exception e)
            {
                throw new Exception("An error occured while consuming the message through RabbitMQ.", e);
            }
        }

        public void OnConsumeMessage(string message)
        {
            Console.WriteLine(" [x] Received {0}", message);
        }
    }
}
