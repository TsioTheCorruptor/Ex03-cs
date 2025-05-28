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
        public void SetEnergyPercentage(float i_Percentage)
        {
            float decimalPercentage=i_Percentage/100;
            if (decimalPercentage < 0f || decimalPercentage > 1f)
            {
                string message = $"Error: Energy percentage must be between 0 and 100. Received: {i_Percentage}";
                throw new ValueRangeException(message, 0f, 1f);
            }

            m_RemaningEnergy = r_MinimumEnergy + decimalPercentage * (r_MaximumEnergy - r_MinimumEnergy);
        }

    }
}
