using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Application.Orders.Dto
{
    public class UpdateOrderStatusDto
    {
        public int Id { get; set; }
        public ApplicationTaskStatus State { get; set; }
    }
}
