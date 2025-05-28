using Enums;
using Ex03.GarageLogic.Combustive;
using Ex03.GarageLogic.BaseVehicleTypes;

namespace Ex03.GarageLogic
{
    public class Truck: BaseTruck, ICombustive
    {
        private const float k_fuelTankVolume = 135f;
        private const float k_minimumFuelAmount = 0;
        private const float k_initialFuelAmount = 0;
        private const FuelType k_DefaultFuelType = FuelType.Soler;
        public Truck(string i_modelName, string i_licansePlate)
            : base(i_modelName, i_licansePlate, new CombustivePowertrain(k_minimumFuelAmount, k_fuelTankVolume, k_initialFuelAmount, k_DefaultFuelType))
        {

        }

        public float GetMaxFuelAmount()
        {
            return ((ICombustive)this.m_powerTrain).GetMaxFuelAmount();
        }
        public float GetFuelRemainingAmount()
        {
            return ((ICombustive)this.m_powerTrain).GetFuelRemainingAmount();
        }
        public FuelType GetFuelType()
        {
            return ((ICombustive)this.m_powerTrain).GetFuelType();
        }
        public void Fuel(FuelType i_fuelType, float i_literFuelAmount)
        {
            ((ICombustive)this.m_powerTrain).Fuel(i_fuelType, i_literFuelAmount);
        }
    }
}
