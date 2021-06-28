using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemMonitor.Application.Orders.Dto
{
    /// <summary>
    /// Class was auto generated from json
    /// For test task this should be acceptable but should be used automapper
    /// With propper names and types
    /// </summary>
    public class OrderDto
    {
        public int id { get; set; }
        public string order_note { get; set; }
        public string order_type { get; set; }
        public string waiter { get; set; }
        public string table { get; set; }
        public DateTime ordered_at { get; set; }
        public List<OrderItemDto> items { get; set; }

        // this is added manually (not auto generated)
        public int customers_count => items
            .Select(item => item.client)
            .Distinct()
            .Count();
    }
}
