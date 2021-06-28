using System;
using System.Threading;
using System.Threading.Tasks;
using SystemMonitor.Api.Interfaces;

namespace SystemMonitor.Api
{
    public class ApiClient : IApiClient
    {
        protected Uri _baseUrl;
        public ApiClient()
        {
        }

        public ApiClient(Uri baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void SetBaseUrl(Uri baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public virtual async Task<TResponse> GetAsync<TResponse>(
            string url = "", 
            object query = null,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            var request = new ApiGetRequest(_baseUrl)
                .ForUrl(url)
                .WithQuery(query);

            await request.SendAsync(cancellationToken);
            return request.GetResult<TResponse>();
        }

        public virtual async Task<TResponse> PostAsync<TResponse>(
            string url = "", 
            object body = null,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            var request = new ApiPostRequest(_baseUrl)
                .ForUrl(url)
                .WithBody(body);

            await request.SendAsync(cancellationToken);
            return request.GetResult<TResponse>();
        }

        public virtual async Task PostAsync(
            string url = "", 
            object body = null,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            var request = new ApiPostRequest(_baseUrl)
                .ForUrl(url)
                .WithBody(body);

            await request.SendAsync(cancellationToken);
            request.GetResult();
        }

        public virtual async Task PutAsync(
            string url = "", 
            object body = null,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            var request = new ApiPutRequest(_baseUrl)
                .ForUrl(url)
                .WithBody(body);

            await request.SendAsync(cancellationToken);
            request.GetResult();
        }

        public virtual async Task DeleteAsync(
            string url = "",
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            var request = new ApiDeleteRequest(_baseUrl)
                .ForUrl(url);

            await request.SendAsync(cancellationToken);
            request.GetResult();
        }
    }
}
