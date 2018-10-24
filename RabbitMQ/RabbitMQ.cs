using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    public class RabbitMQ
    {
        //Default username and password are guest for RabbitMQ.
        private const string ConnectionString = "host=localhost;username;guest;password=guest;";
        private string _message = string.Empty;
        private IConnection _connection = null;
        private IModel _channel = null;

        ~RabbitMQ()
        {
            this.CloseConnection();
        }

        public IModel CreateConnection()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            this._connection = factory.CreateConnection();
            this._channel = _connection.CreateModel();

            return _channel; 
        }

        public void CloseConnection()
        {
            this._connection.Close();
            this._channel.Close();
        }

        /// <summary>
        /// Declaring a queue is idempotent - it will only be created if it doesn't exist already. 
        /// </summary>
        /// <param name="channel"></param>
        public void DeclareQueue(IModel channel)
        {
            channel.QueueDeclare(queue: RabbitConstants.Queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
        }

        public void MessageBrokerPublish(string message)
        {
            Producer Rp = new Producer();
            this.CreateConnection();
            Rp.PublishMessage(_channel, message);
        }

        public void MessageBrokerConsume(string message)
        {
            Consumer Rc = new Consumer();
            this.CreateConnection();
            Rc.ConsumeMessage(_channel, delegate callback);
        }
    }
}
