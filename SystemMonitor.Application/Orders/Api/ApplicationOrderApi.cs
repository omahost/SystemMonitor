using Prism.Events;
using System.Threading;
using System.Threading.Tasks;
using SystemMonitor.Api;
using SystemMonitor.Application.Interfaces.Tasks;
using SystemMonitor.Application.Interfaces.Tasks.Events;
using SystemMonitor.Application.Orders.Dto;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Application.Orders
{
    public class ApplicationOrderApi 
        : ApiClient
        , IApplicationOrderApi
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplicationTaskFacade _applicationTaskFacade;

        public ApplicationOrderApi(
            IEventAggregator eventAggregator,
            IApplicationTaskFacade applicationTaskFacade
            )
        {
            _eventAggregator = eventAggregator;
            _applicationTaskFacade = applicationTaskFacade;

            SetBaseUrl(_applicationTaskFacade.GetEndpointUrl());

            _eventAggregator
                .GetEvent<ApplicationTasksEndpointUrlChanged>()
                .Subscribe(OnEndpointUrlChanged);
        }

        private void OnEndpointUrlChanged(ApplicationTasksEndpointUrlChangedEventArgs args)
        {
            SetBaseUrl(args.EndpointUrl);
        }

        public Task<NewOrdersDto> PollNewOrdersAsync(CancellationToken cancellationToken)
        {
            return GetAsync<NewOrdersDto>(string.Empty, null, cancellationToken);
        }

        public Task SetOrderAsCanceledAsync(int orderId)
        {
            return SetOrderStatusAsync(orderId, ApplicationTaskStatus.Canceled);
        }

        public Task SetOrderAsPrintedAsync(int orderId)
        {
            return SetOrderStatusAsync(orderId, ApplicationTaskStatus.Printed);
        }

        private Task SetOrderStatusAsync(int orderId, ApplicationTaskStatus status)
        {
            return PostAsync(string.Empty, new UpdateOrderStatusDto
            {
                Id = orderId,
                State = status
            });
        }
    }
}
