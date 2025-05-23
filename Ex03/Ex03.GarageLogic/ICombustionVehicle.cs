using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal interface ICombustionVehicle
    {
        public void Fuel(string i_fuelType, float i_literFuelAmount);
    }
}
