using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums ///add enums to specific classes as public instead
{
    public enum FuelType { Soler, Octan95, Octan96, Octan98}

    public enum e_licenseType
    {
        A, A2, AB, B2
    }
    public enum e_FuelType
    {
        Octan95, Octan96, Octan98, Soler
    }
    public enum e_carColors
    {
        black,
        white,
        silver,
        yellow
    }
    public enum e_VehicleTypes
    {
        FuelCar,
        ElectricCar,
        FuelMotorcycle,
        ElectricMotorcycle,
        Truck
    }
    public enum e_VehicleDataKeys
    {
        LicensePlate,
        VehicleType,
        ModelName,
        WheelManufacturer,
        CurrentAirPressure,
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
