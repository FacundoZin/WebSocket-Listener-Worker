using WebSocket_Listener_Worker.src.Listeners;

namespace WebSocket_Listener_Worker.src.Services
{
    public class Worker : BackgroundService
    {
        private readonly WebSocketListener _WebsocketListener;

        public Worker(WebSocketListener webSocketListener)
        {
            _WebsocketListener = webSocketListener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _WebsocketListener.Start();
        }
    }
}
