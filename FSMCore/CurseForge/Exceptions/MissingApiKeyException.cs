using System;

namespace CurseForge.APIClient.Exceptions
{
    internal class MissingApiKeyException : Exception
    {
        public MissingApiKeyException(string message) : base(message) { }
    }
}
