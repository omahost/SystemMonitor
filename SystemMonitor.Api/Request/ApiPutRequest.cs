using System;

namespace SystemMonitor.Api
{
    public class ApiPutRequest : ApiRequestWithBody<ApiPutRequest>
    {
        public ApiPutRequest(Uri baseUrl)
            : base(baseUrl, "PUT")
        {
        }
    }
}
