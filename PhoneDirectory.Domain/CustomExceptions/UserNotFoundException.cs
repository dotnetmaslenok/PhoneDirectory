using PhoneDirectory.Domain.BaseExceptions;

namespace PhoneDirectory.Domain.CustomExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId)
            : base($"User with the identifier {userId} was not found")
        {
        }
    }
}