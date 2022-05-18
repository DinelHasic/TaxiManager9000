using TaxiManager9000.Domain.Enums;
using TaxiManager9000.Domain.Exceptions;

namespace TaxiManager9000.UI.Utils
{
    public static class EnumUtils
    {
        public static Role ConvertRole(this string role)
        {
            bool isRole = Enum.TryParse(role, out Role roleParse);

            if (!isRole)
            {
                throw new Exception("Role invalid");
            }

            return roleParse;
        }

        public static Shift ConvertShift(this string shift)
        {
            bool isRole = Enum.TryParse(shift, out Shift shiftParse);

            if (!isRole)
            {
                throw new Exception("Shift invalid");
            }

            return shiftParse;
        }
    }
}
