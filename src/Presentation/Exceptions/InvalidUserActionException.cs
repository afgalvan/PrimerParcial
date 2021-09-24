using System;

namespace Presentation.Exceptions
{
    public class InvalidUserActionException : Exception
    {
        public InvalidUserActionException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public InvalidUserActionException(string message) : base(message)
        {
        }
    }
}
