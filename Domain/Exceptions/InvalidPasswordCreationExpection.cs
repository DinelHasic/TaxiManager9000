namespace TaxiManager9000.Domain.Exceptions
{
    public class InvalidPasswordCreationExpection : Exception
    {
        public InvalidPasswordCreationExpection(string mrssage) : base(mrssage)
        {
        }
    }
}
