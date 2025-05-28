using Ex03.GarageLogic.Components;
using Ex03.GarageLogic.Electric;
using Ex03.GarageLogic.BaseVehicleTypes;

namespace Ex03.GarageLogic
{
    public class ElectricCar: BaseMotorcycle, IElectric
    {
        private const float k_BatteryVolume = 4.8f;
        private const float k_minimumChargeAmount = 0;
        private const float k_initialChargeAmount = 0;
        public ElectricCar(string i_modelName, string i_licansePlate)
            : base(i_modelName, i_licansePlate, new ElectricPowertrain(k_minimumChargeAmount, k_BatteryVolume, k_initialChargeAmount))
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
