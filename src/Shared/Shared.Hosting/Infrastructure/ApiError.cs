using System.Collections.Generic;

namespace Shared.Hosting.Infrastructure
{
    public class ApiError
    {
        public int StatusCode { get; }
        public IDictionary<string, IEnumerable<string>> Errors { get; }

        public ApiError(int statusCode = 500)
        {
            StatusCode = statusCode;
        }

        public ApiError(int statusCode, string field, string error) : this(statusCode)
        {
            Errors = new Dictionary<string, IEnumerable<string>>
            {
                {field, new[] {error}}
            };
        }

        public ApiError(int statusCode, IDictionary<string, IEnumerable<string>> errors) : this(statusCode)
        {
            Errors = errors;
        }
    }
}