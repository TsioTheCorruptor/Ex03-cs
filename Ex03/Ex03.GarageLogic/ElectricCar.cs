using Ex03.GarageLogic.Components;
using Ex03.GarageLogic.Electric;
using Ex03.GarageLogic.VehicleTypes;

namespace Ex03.GarageLogic
{
    public class ElectricCar: Motorcycle, IElectric
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
            return ((IElectric)this.m_powerTrain).GetMaxCharge();
        }

        public float GetRemaningCharge()
        {
            return ((IElectric)this.m_powerTrain).GetRemaningCharge();
        }

        public void Charge(float i_time)
        {
            ((IElectric)this.m_powerTrain).Charge(i_time);
        }
    }
}
