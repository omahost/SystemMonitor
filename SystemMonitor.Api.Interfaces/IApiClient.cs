using System.Threading;
using System.Threading.Tasks;

namespace SystemMonitor.Api.Interfaces
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(
            string url, object query = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TResponse> PostAsync<TResponse>(
            string url, object body = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task PostAsync(
            string url, object body = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task PutAsync(
            string url, object body = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            string url,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
