using BaseComponents;
using CostumExceptions;
using Ex03.GarageLogic.Electric;

namespace Ex03.GarageLogic.Components
{
    public class ElectricPowertrain: BasePowertrain, IElectric
    {
        public ElectricPowertrain(float i_minimumChargeLevel, float i_maxChargeLevel, float i_initialChargeLevel) : base(i_minimumChargeLevel, i_maxChargeLevel, i_initialChargeLevel)
        {

        }

        public float GetRemaningCharge()
        {
            return this.m_RemaningEnergy;
        }

        public float GetMaxCharge()
        {
            return this.r_MaximumEnergy;
        }

        public void Charge(float i_timeToCharge)
        {
            if (i_timeToCharge < 0)
            {
                string messege = $"Error: cannot discharge battary using 'Charge' method. actual charge given to Charge : {i_timeToCharge}";
                throw new ArgumentException(messege);
            }

            float resultingRemaningCharge = this.m_RemaningEnergy + i_timeToCharge;
            if (resultingRemaningCharge <= this.r_MaximumEnergy && resultingRemaningCharge >= this.r_MinimumEnergy)
            {
                this.m_RemaningEnergy = resultingRemaningCharge;
            }
            else
            {
                string messege = $"Error: Battery charge exceeded allowed range. range: from {r_MinimumEnergy} Hrs to {r_MaximumEnergy} Hrs,  but was set to {resultingRemaningCharge} Hrs";
                throw new ValueRangeException(messege, r_MinimumEnergy, r_MaximumEnergy);
            }
        }
    }
}
