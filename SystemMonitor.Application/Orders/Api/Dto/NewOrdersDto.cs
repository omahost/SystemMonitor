using System.Collections.Generic;

namespace SystemMonitor.Application.Orders.Dto
{
    /// <summary>
    /// Class was auto generated from json
    /// For test task this should be acceptable but should be used automapper
    /// With propper names and types
    /// </summary>
    public class NewOrdersDto
    {
        public string company { get; set; }
        public string tax_id { get; set; }
        public string generated_at { get; set; }
        public List<CheckDto> checks { get; set; }
        public OrdersDto orders { get; set; }
    }
}
