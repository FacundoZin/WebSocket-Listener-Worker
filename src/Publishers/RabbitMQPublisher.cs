using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace WebSocket_Listener_Worker.src.Publishers
{
    public class RabbitMQPublisher
    {
        private IChannel _channel;

        public async Task InitAsync(IConfiguration Config)
        {
            var factory = new ConnectionFactory
            {
                HostName = Config["RabbitMQ:host"],
                UserName = Config["RabbitMQ:User"],
                Password = Config["RabbitMQ:Password"]
            };

            var connection = await factory.CreateConnectionAsync();
            _channel = await connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync("stock_prices", ExchangeType.Topic, durable: true);
        }

        public async Task Publish(string routingKey, object message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var props = new BasicProperties
            {
                ContentType = "application/json",   
                DeliveryMode = DeliveryModes.Persistent
            };

            await _channel.BasicPublishAsync(
                exchange: "stock_prices",
                routingKey: routingKey,
                mandatory: false,
                basicProperties: props,
                body: body
                );
        }
    }
}
