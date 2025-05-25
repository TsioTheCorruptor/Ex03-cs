using BaseComponents;
using CostumExceptions;
using Enums;
using Ex03.GarageLogic.Combustive;


namespace Ex03.GarageLogic
{
    internal class CombustivePowertrain: BasePowertrain, ICombustive
    {
        private readonly FuelType m_fuelType;
        public CombustivePowertrain(float i_minFuelAmount,float i_maxFuelAmount, float i_initialFuelAmount, FuelType i_fuelType) 
        {
            this.m_minimumEnergy = i_minFuelAmount;
            this.m_maximumEnergy = i_maxFuelAmount;
            this.m_remaningEnergy = i_initialFuelAmount;
            this.m_fuelType = i_fuelType;
        }
        public float GetMaxFuelAmount()
        {
            return this.m_maximumEnergy;
        }

        public float GetFuelRemainingAmount()
        {
            return this.m_remaningEnergy;
        }

        public FuelType GetFuelType()
        {
            return this.m_fuelType;
        }

        public void Fuel(FuelType i_fuelType, float i_literFuelAmount)
        {
            if(i_fuelType != this.m_fuelType)
            {
                string messege = $"Error: Wrong fuel type selected to fuel the vehicle expexted: {this.m_fuelType} got {i_fuelType}";
                throw new ArgumentException(messege);
            }


            if (i_literFuelAmount < 0)
            {
                string messege = $"Error: cannot remove fuel using 'Fuel' method. actual amout given to refuel : {i_literFuelAmount}";
                throw new ArgumentException(messege);
            }

            float resultingRemaningFuel = this.m_remaningEnergy + i_literFuelAmount;
            if (resultingRemaningFuel <= this.m_maximumEnergy && resultingRemaningFuel >= this.m_minimumEnergy)
            {
                this.m_remaningEnergy = resultingRemaningFuel;
            }
            else
            {
                string messege = $"Error: Fuel capacity exceeded allowed range. range: from {m_minimumEnergy} Liters to {m_maximumEnergy} Liters,  but was set to {resultingRemaningFuel} Liters";
                throw new ValueRangeException(messege, m_minimumEnergy, m_maximumEnergy);
            }
        }
    }
}
