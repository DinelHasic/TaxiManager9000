using TaxiManager9000.Domain.Exceptions;

namespace TaxiManager9000.UI.Utils
{
    public static class NumberUtils
    {
        public static int ConverToNumber(this string value)
        {
            bool isNumber = int.TryParse(value, out int numberParse);

            if (!(isNumber))
            {
                throw new InvalidCredentialsException("Invalid Id");
            }

            return numberParse;
        }
    }
}
