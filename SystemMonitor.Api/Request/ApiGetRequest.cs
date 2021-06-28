using System;

namespace SystemMonitor.Api
{
    public class ApiGetRequest : ApiRequest<ApiGetRequest>
    {
        public ApiGetRequest(Uri baseUrl)
            : base(baseUrl)
        {
        }
    }
}
