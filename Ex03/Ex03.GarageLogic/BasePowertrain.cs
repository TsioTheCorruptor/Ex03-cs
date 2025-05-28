using CostumExceptions;

namespace BaseComponents
{
    public class BasePowertrain
    {
        protected readonly float r_MinimumEnergy;
        protected readonly float r_MaximumEnergy;
        protected float m_RemaningEnergy;
        
        public BasePowertrain(float i_minimumEnergy, float i_maximumEnergy, float i_remaningEnergy)
        {
            if (i_minimumEnergy < 0)
            {
                string messege = $"Error: Cannot set minimum powertrain energy below 0: got {i_minimumEnergy}";
                throw new ArgumentException(messege);
            }
            if (i_minimumEnergy >= i_maximumEnergy)
            {
                string messege = $"Error: Cannot set Maximum powertrain energy below or equal to Minimum powertrain energy: Minimum: {i_minimumEnergy} Maximum: {i_maximumEnergy}";
                throw new ArgumentException(messege);
            }
            if (i_remaningEnergy > i_maximumEnergy || i_remaningEnergy < r_MinimumEnergy)
            {
                string messege = $"Error: Initial power train energy was set out of range: from {i_minimumEnergy} Units to {i_maximumEnergy} Units,  but was set to {i_remaningEnergy} Hrs";
                throw new ValueRangeException(messege, i_minimumEnergy, i_maximumEnergy);
            }
            r_MinimumEnergy = i_minimumEnergy;
            r_MaximumEnergy = i_maximumEnergy;
            m_RemaningEnergy = i_remaningEnergy;
        }

        public float GetEnergyPrecentage()
        { 
            return (m_RemaningEnergy - r_MinimumEnergy) / (r_MaximumEnergy - r_MinimumEnergy);
        }

    }
}
