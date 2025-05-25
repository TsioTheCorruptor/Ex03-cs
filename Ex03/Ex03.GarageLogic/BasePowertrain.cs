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
            m_minimumEnergy = i_minimumEnergy;
            m_maximumEnergy = i_maximumEnergy;
            m_remaningEnergy = i_remaningEnergy;
        }
    }
}
