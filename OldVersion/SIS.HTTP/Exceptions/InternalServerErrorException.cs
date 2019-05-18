using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    class InternalServerErrorException : Exception
    {
        private const string InternalServerErorrExceptionDefaultMessage = "The Server has encountered an error.";

        public InternalServerErrorException() : this(InternalServerErorrExceptionDefaultMessage)
        {
            
        }

        public InternalServerErrorException(string name) : base(name)
        {
            
        }
    }
}
