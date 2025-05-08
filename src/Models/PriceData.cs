using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocket_Listener_Worker.src.Models
{
    public class PriceData
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
