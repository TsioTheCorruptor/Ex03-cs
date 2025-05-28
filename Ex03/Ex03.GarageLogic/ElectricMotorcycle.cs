using Enums;
using Ex03.GarageLogic.Components;
using Ex03.GarageLogic.Electric;
using Ex03.GarageLogic.BaseVehicleTypes;

namespace Ex03.GarageLogic.Vehicles
{
    public class ElectricMotorcycle: BaseMotorcycle, IElectric
    {
        private const float k_BatteryVolume = 3.2f;
        private const float k_MinimumChargeAmount = 0;
        private const float k_InitialChargeAmount = 0;
        public ElectricMotorcycle(string i_modelName, string i_licansePlate) 
            : base(i_modelName, i_licansePlate, new ElectricPowertrain(k_MinimumChargeAmount, k_BatteryVolume, k_InitialChargeAmount))
        {

        }
        //add other methods

        public float GetMaxCharge()
        {
            return ((IElectric)this.r_PowerTrain).GetMaxCharge();
        }

        public float GetRemaningCharge()
        {
            return ((IElectric)this.r_PowerTrain).GetRemaningCharge();
        }

        public void Charge(float i_time)
        {
            ((IElectric)this.r_PowerTrain).Charge(i_time);
        }
    }
}
