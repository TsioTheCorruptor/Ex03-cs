using Enums;
using Ex03.GarageLogic.Combustive;
using Ex03.GarageLogic.VehicleTypes;


namespace Ex03.GarageLogic.Vehicles
{
    public class FuelMotorcycle: Motorcycle, ICombustive
    {
        public FuelMotorcycle(string i_modelName, string i_licansePlate)
            : base(i_modelName, i_licansePlate, new CombustivePowertrain(0, (float)5.8, 0, FuelType.Octan98))
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
