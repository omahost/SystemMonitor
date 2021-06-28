using Prism.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SystemMonitor.Application.Interfaces.App.Events;
using SystemMonitor.Application.Interfaces.Orders.Events;
using SystemMonitor.Application.Interfaces.Tasks.Events;
using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Application.Orders
{
    public class ApplicationOrderPoller : IApplicationOrderPoller
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplicationOrderApi _applicationOrderApi;
        private readonly IApplicationOrderApiMapper _applicationOrderApiMapper;

        public ApplicationOrderPoller(
            IEventAggregator eventAggregator,
            IApplicationOrderApi applicationOrderApi,
            IApplicationOrderApiMapper applicationOrderApiMapper
            )
        {
            _eventAggregator = eventAggregator;
            _applicationOrderApi = applicationOrderApi;
            _applicationOrderApiMapper = applicationOrderApiMapper;

            _eventAggregator
                .GetEvent<ApplicationTasksEndpointUrlChanged>()
                .Subscribe(OnEndpointUrlChanged);

            _eventAggregator
                .GetEvent<ApplicationClosing>()
                .Subscribe(OnApplicationClosing);
        }

        private void OnApplicationClosing(ApplicationClosingEventArgs args)
        {
            StopPolling();
        }

        private CancellationTokenSource _cancellationTokenSource;
        private void OnEndpointUrlChanged(ApplicationTasksEndpointUrlChangedEventArgs args)
        {
            StartPolling();
        }

        public void Initialize()
        {
            StartPolling();
        }

        private void StartPolling()
        {
            _ = StartPollingAsync();
        }

        private async Task StartPollingAsync()
        {
            StopPolling();

            var cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource = cancellationTokenSource;

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                await PollNewOrdersAsync();
            }
        }

        private async Task PollNewOrdersAsync()
        {
            try
            {
                var ordersDtos = await _applicationOrderApi.PollNewOrdersAsync(
                    _cancellationTokenSource.Token);

                var orders = _applicationOrderApiMapper.Map(ordersDtos);

                TriggerOrdersReceived(orders);
            }
            catch (Exception)
            {
                // TODO: log
            }
        }

        private void StopPolling()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
        }

        private void TriggerOrdersReceived(List<IApplicationOrder> orders)
        {
            _eventAggregator
                .GetEvent<ApplicationOrderReceived>()
                .Publish(new ApplicationOrderReceivedEventArgs(orders));
        }
    }
}
