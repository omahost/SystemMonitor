namespace SystemMonitor.Application.Orders.Dto
{
    /// <summary>
    /// Class was auto generated from json
    /// For test task this should be acceptable but should be used automapper
    /// With propper names and types
    /// </summary>
    public class OrderItemDto
    {
        public string name { get; set; }
        public double price_gross { get; set; }
        public double vat { get; set; }
        public string stawka { get; set; }
        public int quantity { get; set; }
        public double amount { get; set; }
        public string note { get; set; }
        public int rank { get; set; }
        public string client { get; set; }
    }
}
