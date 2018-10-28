using System;

namespace HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string MESSAGE = "The Request was malformed or contains unsupported elements.";

        public BadRequestException() : base(MESSAGE) { }
    }
}