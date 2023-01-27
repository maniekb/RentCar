using CarRent.Domain.Enums;

namespace CarRent.Common.Authentication.Helpers
{
    public static class CarClassHelper
    {
        public static string ResolveCarClass(CarClassEnum carType)
        {
            switch (carType)
            {
                case CarClassEnum.BUDGET:
                    return "Budzet";
                case CarClassEnum.NORMAL:
                    return "Normalne";
                default:
                    return "Premium";
            }
        }
    }
}
