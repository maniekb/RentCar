using CarRent.Domain.Enums;

namespace CarRent.Common.Authentication.Helpers
{
    public static class CarHelper
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

        public static string ResolveFuelType(FuelTypeEnum fuelType)
        {
            switch (fuelType)
            {
                case FuelTypeEnum.PETROL:
                    return "Benzyna";
                case FuelTypeEnum.DIESEL:
                    return "Diesel";
                case FuelTypeEnum.HYBRID:
                    return "Hybryda";
                default:
                    return "Elektryczny";
            }
        }
    }
}
