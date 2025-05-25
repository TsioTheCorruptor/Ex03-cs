using System;
using Vehicle;
using Enums;

namespace Ex03.GarageLogic.VehicleTypes
{
    public class Motorcycle: BaseVehicle
    {
        public readonly MotorcycleLicenseType m_licenceType;
        public readonly int m_engineVolume;
    }
}
