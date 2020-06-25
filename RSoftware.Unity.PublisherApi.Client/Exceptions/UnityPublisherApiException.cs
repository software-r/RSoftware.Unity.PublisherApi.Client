using System;
using System.Net;

namespace RSoftware.Unity.PublisherApi.Client.Exceptions
{
    public class UnityPublisherApiException : Exception
    {
        public HttpStatusCode ErrorCode { get; }

        public UnityPublisherApiException(string message) : base(message)
        {
        }

        public UnityPublisherApiException(string message, HttpStatusCode code) : base(message)
        {
            ErrorCode = code;
        }
    }
}
