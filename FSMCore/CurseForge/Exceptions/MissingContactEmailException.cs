using System;

namespace CurseForge.APIClient.Exceptions
{
    internal class MissingContactEmailException : Exception
    {
        public MissingContactEmailException(string message) : base(message) { }
    }
}
