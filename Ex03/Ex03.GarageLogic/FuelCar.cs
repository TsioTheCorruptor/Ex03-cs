using Enums;
using Ex03.GarageLogic.Combustive;
using Ex03.GarageLogic.BaseVehicleTypes;

namespace Ex03.GarageLogic
{
    internal class FuelCar: BaseCar, ICombustive
    {
        private const float k_FuelTankVolume = 48f;
        private const float k_MinimumFuelAmount = 0;
        private const float k_InitialFuelAmount = 0;
        private const FuelType k_DefaultFuelType = FuelType.Octan95;
        public FuelCar(string i_modelName, string i_licansePlate)
            : base(i_modelName, i_licansePlate, new CombustivePowertrain(k_MinimumFuelAmount, k_FuelTankVolume, k_InitialFuelAmount, k_DefaultFuelType))
        {

        }

        public float GetMaxFuelAmount()
        {
            return ((ICombustive)this.r_PowerTrain).GetMaxFuelAmount();
        }
        public float GetFuelRemainingAmount()
        {
            return ((ICombustive)this.r_PowerTrain).GetFuelRemainingAmount();
        }
        public FuelType GetFuelType()
        {
            return ((ICombustive)this.r_PowerTrain).GetFuelType();
        }
        public void Fuel(FuelType i_fuelType, float i_literFuelAmount)
        {
            ((ICombustive)this.r_PowerTrain).Fuel(i_fuelType, i_literFuelAmount);
        }
    }
}
