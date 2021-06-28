using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Authentication;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading;
using SystemMonitor.Api.Extensions;
using System.Threading.Tasks;

namespace SystemMonitor.Api
{
    public abstract class ApiRequest<TApiRequest>
        where TApiRequest : ApiRequest<TApiRequest>
    {
        private string _requestUrl;
        private string _requestQuery;
        private string _responseBody;
        private readonly Uri _baseUrl;
        private HttpWebRequest _request;

        public ApiRequest(Uri baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected HttpWebRequest Request => _request;

        public TApiRequest ForUrl(string requestUrl)
        {
            _requestUrl = GetUrl(requestUrl);
            return (TApiRequest)this;
        }

        private int _retryCount = 3;
        public TApiRequest WithRetryCount(int retryCount)
        {
            if (retryCount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _retryCount = retryCount;
            return (TApiRequest)this;
        }

        private int _retryIndex = 0;
        public async Task<TApiRequest> SendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                CreateRequest();
                InitializeRequest(_request);
                await ReadResponseAsync(cancellationToken);
                return (TApiRequest)this;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                if (++_retryIndex >= _retryCount)
                {
                    throw;
                }
                return await SendAsync(cancellationToken);
            }
        }

        public TResult GetResult<TResult>()
        {
            if (typeof (TResult) == typeof(string))
            {
                return (TResult)(object)_responseBody;
            }

            return new ApiResult<TResult>(_responseBody)
                .Deserialize();
        }

        public void GetResult()
        {
            if (string.IsNullOrEmpty(_responseBody))
            {
                return;
            }

            // TODO: add other checks
            if (_responseBody.Contains("SQLSTATE"))
            {
                throw new InvalidOperationException(_responseBody);
            }
        }

        private void CreateRequest()
        {
            var requestUrl = GetRequestUrl();
            _request = (HttpWebRequest)WebRequest.Create(requestUrl);
            _request.AutomaticDecompression = DecompressionMethods.GZip;
            _request.ServerCertificateValidationCallback += RemoteCertificateValidationCallback;
        }

        private static bool RemoteCertificateValidationCallback(
            object sender, X509Certificate certificate, X509Chain chain, 
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public TApiRequest WithQuery(object query)
        {
            if (query == null)
            {
                _requestQuery = null;
                return (TApiRequest)this;
            }

            var properties = query
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                ;

            var builder = new StringBuilder();
            foreach (var property in properties)
            {
                var value = property.GetValue(query, null);
                if (value == null)
                {
                    continue;
                }

                var valueType = value.GetType();
                var defaultValue = Activator.CreateInstance(valueType);
                if (Equals(defaultValue, value))
                {
                    continue;
                }

                var queryValue = WebUtility.UrlEncode(value.ToString());
                if (builder.Length != 0)
                {
                    builder.Append('&');
                }
                builder.Append($"{property.Name}={queryValue}");
            }

            if (builder.Length == 0)
            {
                return (TApiRequest)this;
            }

            _requestQuery = builder.ToString();
            return (TApiRequest)this;
        }

        public string GetRequestUrl()
        {            
            if (_requestQuery == null)
            {
                return _requestUrl;
            }
            return $"{_requestUrl}?{_requestQuery}";
        }

        protected virtual void InitializeRequest(HttpWebRequest request)
        {
        }

        private async Task ReadResponseAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var httpResponse = await _request.GetResponseAsync(cancellationToken))
                {
                    _responseBody = ReadResponse(httpResponse);
                }
            }
            catch (WebException ex)
            {
                HandleResponseException(ex);
                throw;
            }
        }

        private void HandleResponseException(WebException ex)
        {
            var httpResponse = (HttpWebResponse)ex.Response;
            if (httpResponse == null)
            {
                ThrowInvalidOperationException(ex);
            }

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                ThrowUnauthorizedException(ex);
            }

            var responseBody = ReadResponse(httpResponse);
            HandleResponseException(ex, responseBody);
        }

        private void HandleResponseException(WebException ex, string responseBody)
        {
            ApiResponseDto responseDto;
            try
            {
                responseDto = JsonConvert
                    .DeserializeObject<ApiResponseDto>(responseBody);
            }
            catch
            {
                return;
            }

            var responseError = responseDto?.Error;
            if (responseError == null)
            {
                return;
            }

            if (responseError.Code == (int)HttpStatusCode.Unauthorized)
            {
                ThrowUnauthorizedException(ex);
            }

            ThrowInvalidOperationException(ex, responseError.Message);
        }

        private void ThrowUnauthorizedException(Exception ex)
        {
            throw new AuthenticationException("Unauthorized", ex);
        }

        private void ThrowInvalidOperationException(Exception ex, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = ex.Message;
            }
            throw new InvalidOperationException(message, ex);
        }

        private string ReadResponse(HttpWebResponse httpResponse)
        {
            using (var responseStream = httpResponse.GetResponseStream())
            using (var streamReader = new StreamReader(responseStream))
            {
                return streamReader
                    .ReadToEnd()
                    .Trim();
            }
        }

        protected virtual string GetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = string.Empty;
            }

            if (url.Length != 0 && url[0] != '/')
            {
                url = '/' + url;
            }            

            var baseUrl = BaseUrl.AbsoluteUri;
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }
            return baseUrl + url;
        }

        protected virtual Uri BaseUrl => _baseUrl;
    }
}
