using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using MT.Application.RabbitMq.Interfaces;
using MT.Application.UserService.Interfaces;
using MT.Domain.Entity;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MT.Application.RabbitMq;

public class RabbitMqService: IRabbitMqService
{
    private readonly IConnection _connection;

    public RabbitMqService(string hostName, string userName, string password)
    {
        var factory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };

        _connection = factory.CreateConnection();
        Channel = _connection.CreateModel();
    }

    public IModel Channel { get; }
    
    public void StartConsuming(string queueName, EventHandler<BasicDeliverEventArgs> onMessageReceived)
    {
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += onMessageReceived;
        Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        Channel?.Close();
        _connection?.Close();
    }
}