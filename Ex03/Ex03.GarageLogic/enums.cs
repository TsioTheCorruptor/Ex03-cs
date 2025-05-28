using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums ///add enums to specific classes as public instead
{
    public enum eFuelType { Soler, Octan95, Octan96, Octan98}

    public enum eMotorcycleLicenseType { A, A2, AB, B2 }
    public enum eCarColor { Yellow, Black, White, Silver}
    public enum eGarageEntryStatus { RepairInProgress, Repaired,  PaidFor}
    public enum eSortOptions { No,Yes }





    public enum eVehicleTypes
    {
        FuelCar,
        ElectricCar,
        FuelMotorcycle,
        ElectricMotorcycle,
        Truck
    }
    public enum eVehicleDataKeys
    {
        LicensePlate,
        VehicleType,
        ModelName,
        WheelData,
        MultipleWheelData,
        Color,
        NumberOfDoors,
        LicenseType,
        EngineCapacity,
        FuelType,
        CurrentFuelAmount,
        CurrentBatteryTime,
        IsCarryingDangerousMaterials,
        CargoVolume,
        EnergyPercentage
           
    }

}
