using System.Collections.Generic;

namespace SystemMonitor.Application.Orders.Dto
{
    /// <summary>
    /// Class was auto generated from json
    /// For test task this should be acceptable but should be used automapper
    /// With propper names and types
    /// </summary>
    public class CheckDto
    {
        public int id { get; set; }
        public List<OrderItemDto> items { get; set; }
        public double tax_a { get; set; }
        public double tax_b { get; set; }
        public int tax_c { get; set; }
        public int tax_d { get; set; }
        public double total_amount { get; set; }
        public string tax_id { get; set; }
        public string internal_order_id { get; set; }
        public string name { get; set; }
        public int? price_gross { get; set; }
        public double? vat { get; set; }
        public string stawka { get; set; }
        public int? quantity { get; set; }
        public int? amount { get; set; }
        public string order_id { get; set; }
    }
}
