using System;
using Vehicle;
using Enums;
using BaseComponents;

namespace Ex03.GarageLogic.VehicleTypes
{
    public class Motorcycle: BaseVehicle
    {
        //Given Consts
        public const int k_numOfWheels = 2;
        public const float k_maxWheelsPressure = 30f;
        private const float k_WheelMinPressure = -0f;
        public MotorcycleLicenseType LicenceType { get; set; }
        public int EngineVolume { get; set; }

        public Motorcycle (string i_modelName, string i_licansePlate, BasePowertrain i_powerTrain)
            : base(i_modelName, i_licansePlate, k_numOfWheels, i_powerTrain)
        {

        }
    }
}
