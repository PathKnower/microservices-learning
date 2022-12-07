using MicroservicesLearning.PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MicroservicesLearning.PlatformService.AsyncDataServices
{
    public class RabbitMqClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;

        public RabbitMqClient(IConfiguration configuration)
        {
            _exchangeName = _configuration[Constants.RabbitMQ_EXCHANGE_CONFIG_NAME];
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration[Constants.RabbitMQ_HOST_CONFIG_NAME],
                Port = int.Parse(_configuration[Constants.RabbitMQ_PORT_CONFIG_PORT])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("--> Connected to the Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message bus: {ex.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection shutdown");
        }

        public void PublishNewPlatform(PlatformPublishDto platformPublishDto)
        {
            var message =  JsonSerializer.Serialize(platformPublishDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection open, sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ Connection closed, not sending message.");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(_exchangeName, string.Empty, null, body);
            Console.WriteLine($"--> Message sent {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
            if (_channel.IsOpen)
            {
                _channel.Close();
            }
            if (_connection.IsOpen)
            {
                _connection.Close();
            }
        }
    }
}
