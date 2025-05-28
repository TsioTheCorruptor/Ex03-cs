using Enums;
using Ex03.GarageLogic.Combustive;
using Ex03.GarageLogic.BaseVehicleTypes;


namespace Ex03.GarageLogic.Vehicles
{
    public class FuelMotorcycle: BaseMotorcycle, ICombustive
    {
        private const float k_FuelTankVolume = 5.8f;
        private const float k_MinimumFuelAmount = 0;
        private const float k_InitialFuelAmount = 0;
        private const eFuelType k_DefaultFuelType = eFuelType.Octan98;
        public FuelMotorcycle(string i_modelName, string i_licansePlate)
            : base(i_licansePlate,i_modelName , new CombustivePowertrain(k_MinimumFuelAmount, k_FuelTankVolume, k_InitialFuelAmount, k_DefaultFuelType))
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
        public eFuelType GetFuelType()
        {
            return ((ICombustive)this.r_PowerTrain).GetFuelType();
        }
        public void Fuel(eFuelType i_fuelType, float i_literFuelAmount)
        {
            ((ICombustive)this.r_PowerTrain).Fuel(i_fuelType, i_literFuelAmount);
        }
    }
}
