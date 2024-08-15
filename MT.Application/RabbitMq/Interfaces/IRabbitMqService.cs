using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MT.Application.RabbitMq.Interfaces;

public interface IRabbitMqService: IDisposable
{
    IModel Channel { get; }

    void StartConsuming(string queueName, EventHandler<BasicDeliverEventArgs> onMessageReceived);
}