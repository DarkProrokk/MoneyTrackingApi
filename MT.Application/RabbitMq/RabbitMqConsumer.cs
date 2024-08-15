using System.Diagnostics.Tracing;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MT.Application.RabbitMq.Interfaces;
using MT.Application.RabbitMq.Models;
using MT.Application.UserService.Interfaces;
using MT.Domain.Entity;
using RabbitMQ.Client.Events;

namespace MT.Application.RabbitMq;

public class RabbitMqConsumer
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public RabbitMqConsumer(IRabbitMqService rabbitMqService, IServiceProvider serviceProvider)
    {
        _rabbitMqService = rabbitMqService;
        _serviceProvider = serviceProvider;
        _queueName = "user_registered";
    }

    public void StartConsuming()
    {
        _rabbitMqService.StartConsuming(_queueName, OnMessageReceived);
    }

    private void OnMessageReceived(object sender, BasicDeliverEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Преобразование сообщения в объект
            var registeredEvent = JsonSerializer.Deserialize<RegisteredEvent>(message);

            // Обработка полученного сообщения
            Console.WriteLine($"Received message: {message}");

            var user = new User
            {
                Id = registeredEvent.Guid,
                Email = registeredEvent.Email,
                UserName = registeredEvent.Login
            };
            userService.AddAsync(user);
            // Вызов логики для добавления пользователя

        }
    }
}