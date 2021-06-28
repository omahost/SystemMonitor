using System;
using System.Collections.Generic;
using System.Linq;
using SystemMonitor.Application.Interfaces.Tasks;
using SystemMonitor.Application.Orders.Dto;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Domain.Interfaces.Tasks;
using SystemMonitor.Domain.Orders;

namespace SystemMonitor.Application.Orders
{
    public class ApplicationOrderApiMapper : IApplicationOrderApiMapper
    {
        private readonly IApplicationTaskFacade _applicationTaskFacade;

        public ApplicationOrderApiMapper(
            IApplicationTaskFacade applicationTaskFacade
            )
        {
            _applicationTaskFacade = applicationTaskFacade;
        }

        public List<IApplicationOrder> Map(NewOrdersDto newOrdersDto)
        {
            var ordersDto = newOrdersDto.orders;
            var result = new List<IApplicationOrder>();
            result.AddRange(MapOrders(ordersDto.kitchen, ApplicationTaskType.Kitchen));
            result.AddRange(MapOrders(ordersDto.bar, ApplicationTaskType.Bar));
            return result;
        }

        private IList<IApplicationOrder> MapOrders(List<OrderDto> orderDtos, ApplicationTaskType taskType)
        {
            if (orderDtos == null || orderDtos.Count == 0)
            {
                return new IApplicationOrder[0];
            }

            return orderDtos
                .Select(order => Map(order, taskType))
                .ToList();
        }

        private IApplicationOrder Map(OrderDto orderDto, ApplicationTaskType taskType)
        {
            return new ApplicationOrder
            {
                Id = orderDto.id,
                Task = _applicationTaskFacade.GetTask(taskType),
                TableNumber = orderDto.table,
                OrderedAt = orderDto.ordered_at,
                CustomersCount = orderDto.customers_count,
                OrderNote = orderDto.order_note,
                OrderType = MapOrderType(orderDto.order_type),
                State = ApplicationTaskStatus.Waiting,
                Waiter = orderDto.waiter,
                Items = MapOrderItems(orderDto.items)
            };
        }

        private ApplicationOrderType MapOrderType(string orderType)
        {
            try
            {
                int resultType = 0;

                orderType.Split('|')
                    .Select(value => (int)Enum.Parse(typeof(ApplicationOrderType), value, true))
                    .ToList()
                    .ForEach(value => resultType |= value);

                return (ApplicationOrderType)resultType;
            }
            catch (Exception ex)
            {
                // TODO: log/handle
            }
            return ApplicationOrderType.Unknown;
        }

        private List<IApplicationOrderItem> MapOrderItems(IList<OrderItemDto> orderItems)
        {
            return orderItems
                .Select(item => new ApplicationOrderItem
                {
                    ClientName = item.client,
                    Name = item.name,
                    Note = item.note,
                    Quantity = item.quantity > 0 ? item.quantity : 1,
                    Rank = item.rank
                })
                .ToList<IApplicationOrderItem>();
        }
    }
}
