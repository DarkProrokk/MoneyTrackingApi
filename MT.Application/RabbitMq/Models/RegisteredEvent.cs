namespace MT.Application.RabbitMq.Models;

public class RegisteredEvent
{
    public Guid Guid { get; set; }
    
    public string? Login { get; set; }
    
    public string? Email { get; set; }
}