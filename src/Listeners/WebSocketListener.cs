using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using WebSocket_Listener_Worker.src.Publishers;
using System.Text.Json;
using WebSocket_Listener_Worker.src.Models;
using Microsoft.Extensions.DependencyInjection;
using WebSocket_Listener_Worker.Data;

namespace WebSocket_Listener_Worker.src.Listeners
{
    public class WebSocketListener
    {
        private readonly IConfiguration _Configuration;
        private WebSocket _WebSocket;
        private readonly RabbitMQPublisher _Publisher;
        private ServiceProvider _ServiceProvider;
        
        public WebSocketListener(IConfiguration configuration, RabbitMQPublisher Publisher)
        {
            _Configuration = configuration;
            _Publisher = Publisher;
        }

        public void Start()
        {
            var url = _Configuration["WebSocket:Url"];
            _WebSocket = new WebSocket(url);
            _WebSocket.OnMessage += OnMessageReceived;
            _WebSocket.Connect();
        }

        

        private async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            try
            {
                var message = JsonSerializer.Deserialize<PriceData>(e.Data);
                var routingKey = $"stock.price.{message.Symbol}";
                await _Publisher.Publish(routingKey, message);
            }
            catch (Exception ex)
            {
            }
        }

        private List<string> GetSymbols()
        {
            using var scope = _ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var symbols = context.stocks
                                 .Select(s => s.Symbol)
                                 .Distinct()
                                 .ToList();

            return symbols;
        }

        private void SuscribeToSymbol()
        {
            var symbols = GetSymbols();

            foreach ( var symbol in symbols )
            {
                var subscribeMessage = new
                {
                    type = "subscribe",
                    symbol = symbol
                };

                var jsonMessage = JsonSerializer.Serialize(subscribeMessage);
                _WebSocket.Send(jsonMessage);
            }
        }
    }
}
