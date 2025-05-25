using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Electric
{
    public interface IElectric
    {

        public void Charge(float i_time);
        public float GetRemaningCharge();
        public float GetMaxCharge();

    }
}
