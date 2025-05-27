using BaseComponents;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle;

namespace Ex03.GarageLogic.VehicleTypes
{
    public class Car: Vehicle.Vehicle
    {
        //Given Consts
        private const int k_numOfWheels = 5;
        private const float k_maxWheelsPressure = 32f;
        private const float k_WheelMinPressure = 0f;
        public CarColor CarColor { get; set; }
        public int NumberOfDoors { get; set; }

        public Car(string i_modelName, string i_licansePlate, BasePowertrain i_powerTrain)
            : base(i_modelName, i_licansePlate, k_numOfWheels, k_WheelMinPressure, k_maxWheelsPressure, i_powerTrain)
        {

        }
    }
}
