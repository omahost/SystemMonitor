using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Domain.Orders
{
    /// <summary>
    /// For the test task this is should be acceptable
    /// For real app, should incapsulate/restrict direct field manipulation
    /// </summary>
    public class ApplicationOrderItem : IApplicationOrderItem
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public int Rank { get; set; }
        public int Quantity { get; set; }
        public string ClientName { get; set; }
    }
}
