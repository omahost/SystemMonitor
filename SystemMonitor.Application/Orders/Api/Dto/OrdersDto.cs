using System.Collections.Generic;

namespace SystemMonitor.Application.Orders.Dto
{
    /// <summary>
    /// Class was auto generated from json
    /// For test task this should be acceptable but should be used automapper
    /// With propper names and types
    /// </summary>
    public class OrdersDto
    {
        public List<OrderDto> kitchen { get; set; }
        public List<OrderDto> bar { get; set; }
    }
}
