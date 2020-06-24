using System;

namespace RSoftware.Unity.PublisherApi.Client.Exceptions
{
    public class UnityPublisherApiException : Exception
    {
        public UnityPublisherApiException(string message) : base(message)
        {
        }
    }
}
