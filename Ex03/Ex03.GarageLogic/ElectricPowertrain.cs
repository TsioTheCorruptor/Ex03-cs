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
            return this.m_remaningEnergy;
        }

        public float GetMaxCharge()
        {
            return this.m_maximumEnergy;
        }

        public void Charge(float i_timeToCharge)
        {
            if (i_timeToCharge < 0)
            {
                string messege = $"Error: cannot discharge battary using 'Charge' method. actual charge given to Charge : {i_timeToCharge}";
                throw new ArgumentException(messege);
            }

            float resultingRemaningCharge = this.m_remaningEnergy + i_timeToCharge;
            if (resultingRemaningCharge <= this.m_maximumEnergy && resultingRemaningCharge >= this.m_minimumEnergy)
            {
                this.m_remaningEnergy = resultingRemaningCharge;
            }
            else
            {
                string messege = $"Error: Battery charge exceeded allowed range. range: from {m_minimumEnergy} Hrs to {m_maximumEnergy} Hrs,  but was set to {resultingRemaningCharge} Hrs";
                throw new ValueRangeException(messege, m_minimumEnergy, m_maximumEnergy);
            }
        }
    }
}
