using MicroservicesLearning.CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MicroservicesLearning.CommandsService.AsyncDataServices
{
    public class RabbitMQSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public RabbitMQSubscriber(IConfiguration configuration,
            IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration[Constants.RabbitMQ_HOST_CONFIG_NAME],
                Port = int.Parse(_configuration[Constants.RabbitMQ_PORT_CONFIG_PORT])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_configuration[Constants.RabbitMQ_EXCHANGE_CONFIG_NAME], ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(_queueName, _configuration[Constants.RabbitMQ_EXCHANGE_CONFIG_NAME], string.Empty);

            Console.WriteLine("--> Listening on the Message bus...");

            _connection.ConnectionShutdown += RabbitMq_ConnectionShutdown;
        }

        private void RabbitMq_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Rabbit MQ Connection Shutdown");
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
            }
            if (_connection.IsOpen)
            {
                _connection.Close();
            }

            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += RabbitMqConsumer_Received;

            _channel.BasicConsume(_queueName, true, consumer);

            return Task.CompletedTask;
        }

        private void RabbitMqConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            _eventProcessor.ProcessEvent(notificationMessage);
        }
    }
}
