using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MT.Infrastructure.Data.Context;
using MT.Application.ItemsService;
using MT.Application.ItemsService.Interfaces;
using MT.Application.JwtService;
using MT.Application.JwtService.Interfaces;
using MT.Application.RabbitMq;
using MT.Application.RabbitMq.Interfaces;
using MT.Application.TagsService;
using MT.Application.TagsService.Interfaces;
using MT.Application.UserService;
using MT.Application.UserService.Interfaces;
using MT.Core.Auth;
using MT.Infrastructure.Data.Repository;
using MT.Infrastructure.Data.Repository.Interfaces;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TrackingContext>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();


var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");
var hostName = rabbitMqConfig["HostName"];
var userName = rabbitMqConfig["UserName"];
var password = rabbitMqConfig["Password"];


// var queueName = "user_registered";

// Создание и запуск потребителя сообщений
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagsService, TagsService>();
builder.Services.AddSingleton<IRabbitMqService>(sp => new RabbitMqService(hostName, userName, password));

builder.Services.AddSingleton<RabbitMqConsumer>();

builder.Services.AddDbContext<TrackingContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.Issuer,

        ValidateAudience = true,
        ValidAudience = AuthOptions.Audience,
        
        ValidateLifetime = false,
        
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Извлечение токена из cookies
            if (context.Request.Cookies.TryGetValue("accessToken", out var token))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        }
    };
});
//todo: Настроить корсы
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://127.0.0.1:5500")  
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();  
        });
});
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options => 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize);
var app = builder.Build();

var consumerService = app.Services.GetRequiredService<RabbitMqConsumer>();
consumerService.StartConsuming();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
