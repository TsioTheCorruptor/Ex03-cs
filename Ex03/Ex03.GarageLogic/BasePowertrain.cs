using CostumExceptions;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseComponents
{
    public class BasePowertrain
    {
        protected readonly float m_minimumEnergy;
        protected readonly float m_maximumEnergy;
        protected float m_remaningEnergy;
        
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
            if (i_remaningEnergy > i_maximumEnergy || i_remaningEnergy < m_minimumEnergy)
            {
                string messege = $"Error: Initial power train energy was set out of range: from {i_minimumEnergy} Units to {i_maximumEnergy} Units,  but was set to {i_remaningEnergy} Hrs";
                throw new ValueRangeException(messege, i_minimumEnergy, i_maximumEnergy);
            }
            m_minimumEnergy = i_minimumEnergy;
            m_maximumEnergy = i_maximumEnergy;
            m_remaningEnergy = i_remaningEnergy;
        }

        public float GetEnergyPrecentage()
        { 
            return (m_remaningEnergy - m_minimumEnergy) / (m_maximumEnergy - m_minimumEnergy);
        }

    }
}
