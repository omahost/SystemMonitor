using System;

namespace SystemMonitor.Api
{
    public class ApiPostRequest : ApiRequestWithBody<ApiPostRequest>
    {
        public ApiPostRequest(Uri baseUrl)
            : base(baseUrl, "POST")
        {
        }
    }
}
