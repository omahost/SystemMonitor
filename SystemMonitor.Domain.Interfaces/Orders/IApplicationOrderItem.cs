namespace SystemMonitor.Domain.Interfaces.Orders
{
    public interface IApplicationOrderItem
    {
        string Name { get; }
        string Note { get; }
        int Rank { get; }
        int Quantity { get; }
        string ClientName { get; }
    }
}
