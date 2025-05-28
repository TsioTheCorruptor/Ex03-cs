using BaseComponents;
using CostumExceptions;
using Enums;
using Ex03.GarageLogic.Combustive;


namespace Ex03.GarageLogic
{
    public class CombustivePowertrain: BasePowertrain, ICombustive
    {
        private readonly eFuelType r_FuelType;
        public CombustivePowertrain(float i_minFuelAmount,float i_maxFuelAmount, float i_initialFuelAmount, eFuelType i_fuelType)
            : base(i_minFuelAmount, i_maxFuelAmount, i_initialFuelAmount)
        {
            this.r_FuelType = i_fuelType;
        }
        public float GetMaxFuelAmount()
        {
            return this.r_MaximumEnergy;
        }

        public float GetFuelRemainingAmount()
        {
            return this.m_RemaningEnergy;
        }

        public eFuelType GetFuelType()
        {
            return this.r_FuelType;
        }

        public void Fuel(eFuelType i_fuelType, float i_literFuelAmount)
        {
            if(i_fuelType != this.r_FuelType)
            {
                string messege = $"Error: Wrong fuel type selected to fuel the vehicle expexted: {this.r_FuelType} got {i_fuelType}";
                throw new ArgumentException(messege);
            }


            if (i_literFuelAmount < 0)
            {
                string messege = $"Error: cannot remove fuel using 'Fuel' method. actual amout given to refuel : {i_literFuelAmount}";
                throw new ArgumentException(messege);
            }

            float resultingRemaningFuel = this.m_RemaningEnergy + i_literFuelAmount;
            if (resultingRemaningFuel <= this.r_MaximumEnergy && resultingRemaningFuel >= this.r_MinimumEnergy)
            {
                this.m_RemaningEnergy = resultingRemaningFuel;
            }
            else
            {
                string messege = $"Error: Fuel capacity exceeded allowed range. range: from {r_MinimumEnergy} Liters to {r_MaximumEnergy} Liters,  but was set to {resultingRemaningFuel} Liters";
                throw new ValueRangeException(messege, r_MinimumEnergy, r_MaximumEnergy);
            }
        }
    }
}
