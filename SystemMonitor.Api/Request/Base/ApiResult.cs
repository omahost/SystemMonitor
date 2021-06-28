using Newtonsoft.Json;
using System;
using System.IO;

namespace SystemMonitor.Api
{
    internal class ApiResult<TResult>
    {
        private readonly Type ResultType = typeof(TResult);
        private bool IsResultArray => ResultType.IsArray;

        private string _json;
        public ApiResult(string json)
        {
            _json = json;
            AdaptJson();
        }

        public TResult Deserialize()
        {
            ThrowIfInvalidJson();

            return JsonConvert
                .DeserializeObject<TResult>(_json);
        }

        private void AdaptJson()
        {
            AdaptObjectResponse();
            AdaptArrayResponse();
        }

        private void AdaptObjectResponse()
        {
            if (IsResultArray)
            {
                return;
            }

            if (!_json.StartsWith("["))
            {
                return;
            }

            _json = _json.Trim('[', ']');
        }

        private void AdaptArrayResponse()
        {
            if (!IsResultArray)
            {
                return;
            }

            if (_json.StartsWith("["))
            {
                return;
            }

            _json = $"[{_json}]";
        }

        private void ThrowIfInvalidJson()
        {
            if (_json.Length == 0)
            {
                throw new InvalidDataException("Empty response");
            }

            var firstChar = _json[0];
            // check if it is a json string or not
            if (firstChar != '{' && firstChar != '[')
            {
                throw new InvalidDataException(_json);
            }
        }

        private void LogError(string message)
        {
            // TODO: add log error
        }
    }
}
