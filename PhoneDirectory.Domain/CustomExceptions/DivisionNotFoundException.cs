using PhoneDirectory.Domain.BaseExceptions;

namespace PhoneDirectory.Domain.CustomExceptions
{
    public class DivisionNotFoundException : NotFoundException
    {
        public DivisionNotFoundException(int divisionId) 
            : base($"Division with the identifier {divisionId} was not found")
        {
            
        }
    }
}