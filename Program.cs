using WebSocket_Listener_Worker.src.Publishers;
using WebSocket_Listener_Worker.src.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<RabbitMQPublisher>( serviceProvider => {
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var publisher = new RabbitMQPublisher();
    publisher.InitAsync(config);
    return publisher;
});

var host = builder.Build();
host.Run();
