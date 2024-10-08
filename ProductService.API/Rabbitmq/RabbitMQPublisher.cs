using RabbitMQ.Client;
using System.Text;

namespace ProductService.API.Rabbitmq
{
    public class RabbitMQPublisher
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQPublisher()
        {

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost = "/",


            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
             queue: "product_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
             arguments: null
            );
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "product_queue", basicProperties: null, body: body);
        }
    }

}
