using Microsoft.EntityFrameworkCore;
using WebSocket_Listener_Worker.Data;
using WebSocket_Listener_Worker.src.Listeners;
using WebSocket_Listener_Worker.src.Publishers;
using WebSocket_Listener_Worker.src.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<RabbitMQPublisher>( serviceProvider => {
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var publisher = new RabbitMQPublisher();
    publisher.InitAsync(config).GetAwaiter().GetResult(); ;
    return publisher;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var config = builder.Configuration;
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<WebSocketListener>();

var host = builder.Build();
host.Run();
