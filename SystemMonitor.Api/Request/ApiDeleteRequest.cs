using System;
using System.Net;

namespace SystemMonitor.Api
{
    public class ApiDeleteRequest : ApiRequest<ApiDeleteRequest>
    {
        public ApiDeleteRequest(Uri baseUrl) 
            : base(baseUrl)
        {
        }

        protected override void InitializeRequest(HttpWebRequest request)
        {
            request.Method = "DELETE";
        }
    }
}
