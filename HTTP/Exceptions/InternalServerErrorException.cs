using System;

namespace HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private const string MESSAGE = "The Server has encountered an error.";

        public InternalServerErrorException() : base(MESSAGE) { }
    }
}
