using System;

namespace PhoneDirectory.Domain.BaseExceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        {
            
        }
    }
}