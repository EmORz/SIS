using System;

namespace SIS.HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private const string InternalServerErorrExceptionDefaultMessage = "The Server has encountered an error.";

        public InternalServerErrorException(): this(InternalServerErorrExceptionDefaultMessage)
        {

        }

        public InternalServerErrorException(string name) : base(name)
        {

        }
    }
}
