namespace SystemMonitor.Domain.Interfaces.Orders
{
    public enum ApplicationOrderType
    {
        Unknown = 0,
        Inside = 0x1,
        Outside = 0x2,
        InsideAndOutside = Inside | Outside
    }
}
