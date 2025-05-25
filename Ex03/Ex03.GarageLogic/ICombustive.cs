using Enums;

namespace Ex03.GarageLogic.Combustive
{
    internal interface ICombustive
    {
        public float GetMaxFuelAmount();
        public float GetFuelRemainingAmount();
        public FuelType GetFuelType();
        public void Fuel(FuelType i_fuelType, float i_literFuelAmount);

    }
}
