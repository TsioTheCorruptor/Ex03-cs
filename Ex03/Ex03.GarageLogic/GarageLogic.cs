using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using BaseVehicle;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;
using Ex03.GarageLogic.Combustive;
namespace Ex03.GarageLogic
{
    public class GarageLogic
    {
        Dictionary<string,GarageEntry> m_vehicleDataBase =new Dictionary<string,GarageEntry>();
        Vehicle? m_VehicleBeingCreated;
        public void SetCreatedVehicleAttribute(e_VehicleDataKeys key, string value)
        {
            if (key == e_VehicleDataKeys.WheelData || key == e_VehicleDataKeys.MultipleWheelData)
            {
                SetCreatedWheelAttributes(key, value);
            }
            else
            {
                switch (key)
                {


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




                    case e_VehicleDataKeys.CurrentFuelAmount:
                        if (!float.TryParse(value, out float fuelAmount))
                            throw new FormatException("Invalid format for CurrentFuelAmount.");
                        if (m_VehicleBeingCreated is ICombustive vehicleCar)
                        {
                            vehicleCar.Fuel(vehicleCar.GetFuelType(),fuelAmount); 
                        }
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

                    case e_VehicleDataKeys.EnergyPercentage://delete maybe
                        if (!float.TryParse(value, out float energyPercentage))
                            throw new FormatException("Invalid format for EnergyPercentage.");
                          
                        break;
                    

                    default:
                        throw new ArgumentOutOfRangeException(nameof(key), $"Unhandled key: {key}");
                }
            }
            
        }
        public int GetAmountOfWheelsInVehicleBeingCreated()
        {
            int numOfWheels;
            if(m_VehicleBeingCreated == null)
            {
                throw new NullReferenceException("vehicle not created yet");
            }
            else
            {
                numOfWheels = m_VehicleBeingCreated.m_maxNumOfWheels;
                return numOfWheels;

            }
           
            
            
        }
        public void CreateTempVehicle(string i_licenseNumber,string i_VehicleType,string i_ModelName)
        {
            m_VehicleBeingCreated=VehicleCreator.CreateVehicle(i_VehicleType, i_ModelName,i_ModelName);
        }
        private void AddEntryToDataBase(GarageEntry i_Entry)
        {
            string licensePlate = i_Entry.m_vehicle.m_licansePlate;
            m_vehicleDataBase.Add(licensePlate, i_Entry);
        }
        public GarageEntry CreateEntryFromTempVehicle(string i_PhoneNumber,string i_OwnerName)
        {
            //check if vehicle valid
            GarageEntry? o_Entry=null;
            o_Entry= new GarageEntry(m_VehicleBeingCreated,i_OwnerName, i_PhoneNumber);
            if(o_Entry==null)
            {
                //errrorrrrrr
            }
            else
            {
                m_VehicleBeingCreated = null;

            }
            return o_Entry;
            
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
        public void ChangeVehicleState(string i_State,string i_PlateNumber)
        {
          //change state to string
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
        public bool DoesEntryAlreadyExist(string i_PlateNumber)
        {
           return m_vehicleDataBase.ContainsKey(i_PlateNumber);
        }
        private string[] splitWheelDataSeparatedByComma(string data)
        {
            float pressure;
            string manufacturer;
            string[] splitString;

            if(!data.Contains(","))
            {
                throw new FormatException("invalid format");

            }
             splitString= data.Split(',');
            if(splitString.Length > 2) 
            {
                throw new FormatException("too many instances of ','");
            }
            
            manufacturer = splitString[0];
            if (!float.TryParse(splitString[1], out pressure))
            {
                throw new FormatException("pressure should be float");
            }
            return splitString;
        }
        public void SetCreatedWheelAttributes(e_VehicleDataKeys i_DataKey,string i_WheelData)
        {
string[] splitString = splitWheelDataSeparatedByComma(i_WheelData);
            if(i_DataKey==e_VehicleDataKeys.WheelData)
            {
                m_VehicleBeingCreated.AddWheel(splitString[0], float.Parse(splitString[1]));
            }
            if(i_DataKey ==e_VehicleDataKeys.MultipleWheelData)
            {
                m_VehicleBeingCreated.InitAllWheels(splitString[0], float.Parse(splitString[1]));
            }
        }
    }
}
