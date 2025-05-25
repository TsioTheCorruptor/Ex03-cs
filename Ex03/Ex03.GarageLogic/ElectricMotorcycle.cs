using Ex03.GarageLogic.Components;
using Ex03.GarageLogic.Electric;
using Ex03.GarageLogic.VehicleTypes;

namespace Ex03.GarageLogic.Vehicles
{
    public class ElectricMotorcycle: Motorcycle, IElectric
    {
        public ElectricMotorcycle()
        {
            this.m_powerTrain = new ElectricPowertrain(0, (float)3.2 , 0);
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
