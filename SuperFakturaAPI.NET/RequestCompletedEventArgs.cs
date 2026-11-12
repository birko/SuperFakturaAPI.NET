using System;

namespace Birko.SuperFaktura
{
    // AbstractSuperFaktura.RequestCompleted: one finished API request, for debugging and monitoring.
    // Never carries headers (the Authorization header holds the API key). The invoice token and
    // webhook_secret are shown as "***"; bodies still contain personal data (clients, addresses, IBAN).
    public class RequestCompletedEventArgs : EventArgs
    {
        public string Method { get; }
        // Relative to the API URL.
        public string Uri { get; }
        // null when no response arrived (network error, timeout).
        public int? StatusCode { get; }
        public TimeSpan Duration { get; }
        // Only with IncludeBodiesInEvents: the JSON sent ("data" field of form posts).
        public string RequestBody { get; }
        // Only with IncludeBodiesInEvents; null for binary responses (PDF), see ResponseLength.
        public string ResponseBody { get; }
        public long? ResponseLength { get; }
        // The exception the call throws, or null when it succeeded: Exceptions.Exception for API and
        // HTTP errors, OperationCanceledException when the caller's CancellationToken cancelled it.
        public System.Exception Exception { get; }

        public RequestCompletedEventArgs(string method, string uri, int? statusCode, TimeSpan duration,
            string requestBody, string responseBody, long? responseLength, System.Exception exception)
        {
            Method = method;
            Uri = uri;
            StatusCode = statusCode;
            Duration = duration;
            RequestBody = requestBody;
            ResponseBody = responseBody;
            ResponseLength = responseLength;
            Exception = exception;
        }
    }
}
