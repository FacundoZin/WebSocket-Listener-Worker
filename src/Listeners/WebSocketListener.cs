using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using WebSocket_Listener_Worker.src.Publishers;

namespace WebSocket_Listener_Worker.src.Listeners
{
    public class WebSocketListener
    {
        private readonly IConfiguration _Configuration;
        private WebSocket _WebSocket;
        private readonly RabbitMQPublisher _RabbitMQPublisher;
        
        public WebSocketListener(IConfiguration configuration, RabbitMQPublisher rabbitMQPublisher)
        {
            _Configuration = configuration;
            _RabbitMQPublisher = rabbitMQPublisher;
        }

        public void Start()
        {
            var url = _Configuration["WebSocket:Url"];
            _WebSocket = new WebSocket(url);
            _WebSocket.OnMessage += OnMessageReceived;
            _WebSocket.Connect();
        }

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex)
            {

            }
        }
    }
}
