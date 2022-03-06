using PhoneDirectory.Domain.BaseExceptions;

namespace PhoneDirectory.Domain.CustomExceptions
{
    public class PhoneNumberNotFoundException : NotFoundException
    {
        public PhoneNumberNotFoundException(int phoneNumberId) 
            : base($"Phone number with the identifier {phoneNumberId} was not found")
        {
        }
    }
}