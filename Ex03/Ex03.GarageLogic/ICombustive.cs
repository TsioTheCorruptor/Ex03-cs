using Enums;

namespace Ex03.GarageLogic.Combustive
{
    internal interface ICombustive
    {
        public float GetMaxFuelAmount();
        public float GetFuelRemainingAmount();
        public eFuelType GetFuelType();
        public void Fuel(eFuelType i_FuelType, float i_LiterFuelAmount);

    }
}
