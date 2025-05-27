using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Vehicle;
namespace Ex03.GarageLogic
{
    public class GarageLogic
    {
        Dictionary<string,BaseVehicle> m_vehicleDataBase =new Dictionary<string,BaseVehicle>();
        public void SetCreatedVehicleAttribute(e_VehicleDataKeys key, string value)
        {
            switch (key)
            {
                case e_VehicleDataKeys.LicensePlate:
                    // vehicle.LicensePlate = value;
                    break;

                case e_VehicleDataKeys.VehicleType:
                    // vehicle.VehicleType = value;
                    break;

                case e_VehicleDataKeys.ModelName:
                    // vehicle.ModelName = value;
                    break;

                case e_VehicleDataKeys.WheelManufacturer:
                    // vehicle.WheelManufacturer = value;
                    break;

                case e_VehicleDataKeys.CurrentAirPressure:
                    if (!float.TryParse(value, out float airPressure))
                        throw new FormatException("Invalid format for CurrentAirPressure.");
                    // vehicle.CurrentAirPressure = airPressure;
                    break;

                case e_VehicleDataKeys.Color:
                    
                    // vehicle.Color = color;
                    break;

                case e_VehicleDataKeys.NumberOfDoors:
                    if (!int.TryParse(value, out int numberOfDoors))
                        throw new FormatException("Invalid format for NumberOfDoors.");
                    // vehicle.NumberOfDoors = numberOfDoors;
                    break;

                case e_VehicleDataKeys.LicenseType:
                    
                    // vehicle.LicenseType = licenseType;
                    break;

                case e_VehicleDataKeys.EngineCapacity:
                    if (!int.TryParse(value, out int engineCapacity))
                        throw new FormatException("Invalid format for EngineCapacity.");
                    // vehicle.EngineCapacity = engineCapacity;
                    break;

                case e_VehicleDataKeys.FuelType:
                    
                    // vehicle.FuelType = fuelType;
                    break;

                case e_VehicleDataKeys.CurrentFuelAmount:
                    if (!float.TryParse(value, out float fuelAmount))
                        throw new FormatException("Invalid format for CurrentFuelAmount.");
                    // vehicle.CurrentFuelAmount = fuelAmount;
                    break;

                case e_VehicleDataKeys.CurrentBatteryTime:
                    if (!float.TryParse(value, out float batteryTime))
                        throw new FormatException("Invalid format for CurrentBatteryTime.");
                    // vehicle.CurrentBatteryTime = batteryTime;
                    break;

                case e_VehicleDataKeys.IsCarryingDangerousMaterials:
                    if (!bool.TryParse(value, out bool isCarrying))
                        throw new FormatException("Invalid format for IsCarryingDangerousMaterials.");
                    // vehicle.IsCarryingDangerousMaterials = isCarrying;
                    break;

                case e_VehicleDataKeys.CargoVolume:
                    if (!float.TryParse(value, out float cargoVolume))
                        throw new FormatException("Invalid format for CargoVolume.");
                    // vehicle.CargoVolume = cargoVolume;
                    break;

                case e_VehicleDataKeys.EnergyPercentage:
                    if (!float.TryParse(value, out float energyPercentage))
                        throw new FormatException("Invalid format for EnergyPercentage.");
                    // vehicle.EnergyPercentage = energyPercentage;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(key), $"Unhandled key: {key}");
            }
        }
        public int GetAmountOfWheelsInCurrentCar()
        {
            return 1;
        }
        public void CreateTempVehicle(string i_licenseNumber,string i_VehicleType)
        {

        }
        public void AddVehicleToDataBase(string i_licenseNumber, string i_VehicleType)
        {

        }
        public string getCarEnergyType()
        {
            return "Fuel";
        }
        public e_VehicleDataKeys[] GetPropertiesInfo()
        {
            return new e_VehicleDataKeys[] { e_VehicleDataKeys.IsCarryingDangerousMaterials,e_VehicleDataKeys.Color };
        }
        public void ReadVehicleDataFromFile()
        {

        }
        public void GetLicensePlateList()
        {

        }
        public void ChangeVehicleState()
        {

        }
        public void FillAirInTires()
        {

        }
        public void ChargeFuelVehicle()
        {

        }
        public void ChargeElectricVehicle()
        {

        }
        public void ShowFullVehicleData()
        {

        }



    }
}
