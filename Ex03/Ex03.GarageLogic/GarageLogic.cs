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
using Ex03.GarageLogic.Electric;
using Ex03.GarageLogic.IO;
namespace Ex03.GarageLogic
{
    public class GarageLogic
    {
       public enum eAvailableEnergyTypes
        {
            Fuel,
            Electric
        }
       private Dictionary<string,GarageEntry> m_VehicleDataBase =new Dictionary<string,GarageEntry>();
        Vehicle? m_VehicleBeingCreated;
        readonly string m_VehicleDataFilePath= "Vehicles.db";
        public void SetCreatedVehicleAttribute(eVehicleDataKeys i_Key, string i_Value)
        {
            if (i_Key == eVehicleDataKeys.WheelData || i_Key == eVehicleDataKeys.MultipleWheelData)
            {
                SetCreatedWheelAttributes(i_Key, i_Value);
            }
            else
            {
                switch (i_Key)
                {




                    case eVehicleDataKeys.CurrentFuelAmount:
                        if (!float.TryParse(i_Value, out float fuelAmount))
                        {
                            throw new FormatException("Invalid format for CurrentFuelAmount.");
                        }
                            
                        if (m_VehicleBeingCreated is ICombustive fuelVehicle)
                        {
                            fuelVehicle.Fuel(fuelVehicle.GetFuelType(),fuelAmount); 
                        }
                        break;

                    case eVehicleDataKeys.CurrentBatteryTime:
                        if (!float.TryParse(i_Value, out float batteryTime))
                        {
                            throw new FormatException("Invalid format for CurrentBatteryTime.");
                        }
                           
                        if (m_VehicleBeingCreated is IElectric electricVehicle)
                        {
                            electricVehicle.Charge( batteryTime);
                        }
                        break;

                    
                    

                    default:
                        throw new ArgumentOutOfRangeException(nameof(i_Key), $"Unhandled key: {i_Key}");
                }
            }
            
        }
        public int GetAmountOfWheelsInVehicleBeingCreated()
        {
            int o_NumOfWheels;
            if(m_VehicleBeingCreated == null)
            {
                throw new ArgumentException("not able to get amount of wheels");
            }
            else
            {
                o_NumOfWheels = m_VehicleBeingCreated.GetMaxNumOfWheels();
                return o_NumOfWheels;

            }
           
            
            
        }
        public void CreateTempVehicle(string i_licenseNumber,string i_VehicleType,string i_ModelName)
        {
            m_VehicleBeingCreated=VehicleCreator.CreateVehicle(i_VehicleType, i_licenseNumber,i_ModelName);
        }
        private void AddEntryToDataBase(GarageEntry i_Entry)
        {
            string licensePlate = i_Entry.r_Vehicle.GetLicansePlate();
            m_VehicleDataBase.Add(licensePlate, i_Entry);
        }
        public void CreateEntryFromTempVehicle(string i_PhoneNumber,string i_OwnerName)
        {
            if (m_VehicleBeingCreated == null)
            {
                throw new ArgumentException("cennot modify vehicle");
            }
            //check if vehicle valid
            GarageEntry? o_Entry=null;
            o_Entry= new GarageEntry(m_VehicleBeingCreated,i_OwnerName, i_PhoneNumber);
            if(o_Entry==null)
            {
                throw new NullReferenceException("failed to create entry");
            }
            
            m_VehicleDataBase[o_Entry.r_Vehicle.GetLicansePlate()] =o_Entry;
            
        }
        public eAvailableEnergyTypes getCarEnergyType()
        {
            eAvailableEnergyTypes o_EnergyType;
            if (m_VehicleBeingCreated is ICombustive fuelVehicle)
            {
                o_EnergyType = eAvailableEnergyTypes.Fuel;
            }
            else
            {
                if (m_VehicleBeingCreated is IElectric electricVehicle)
                {
                    o_EnergyType = eAvailableEnergyTypes.Electric;
                }
                else
                {
                    throw new ArgumentException("fuel type not found");
                }

            }

            
            return o_EnergyType;
        }
        public List<string> GetPropertiesInfo()
        {
            if (m_VehicleBeingCreated == null)
            {
                throw new ArgumentException("cennot modify vehicle");
            }

            IReadOnlyList<string> o_stringListTypes= m_VehicleBeingCreated.GetPropertyTypesList();
            IReadOnlyList<string> o_stringListNames = m_VehicleBeingCreated.GetPropertyNamesList();
            List<string> o_listToReturn = new List<string>(o_stringListNames.Count);
            if(o_stringListNames.Count!=o_stringListTypes.Count)
            {
                throw new FormatException("error");
            }
            for (int i = 0; i < o_stringListTypes.Count;i++)
            {
                o_listToReturn.Add(string.Format("{0} as {1}", o_stringListNames[i], o_stringListTypes[i]));
            }
            return o_listToReturn;
        }
        public void SetPropertiesInfo(List<string> i_PropertyString)
        {
            if (m_VehicleBeingCreated == null)
            {
                throw new ArgumentException("cennot modify vehicle");
            }
            m_VehicleBeingCreated.SetAllPropertiesFromOrderdListOfStrings(i_PropertyString);
        }
        public void ReadVehicleDataFromFile()
        {
            IReadOnlyList<GarageEntry> garageEntries;
            
              garageEntries=  VehicleFileImporter.Import(m_VehicleDataFilePath);
            foreach(GarageEntry Entry in garageEntries)
            {
                AddEntryToDataBase(Entry);
            }
            
        }

        public string[] GetLicensePlateList(eGarageEntryStatus i_SortType,eSortOptions i_SortChoice)
        {
            List<string> o_ToReturn=new List<string>();
            if (i_SortChoice == eSortOptions.No)
            {
                o_ToReturn = m_VehicleDataBase.Keys.ToList();
            }
            else
            {
                switch (i_SortType)
                {
                    case eGarageEntryStatus.Repaired:


                        o_ToReturn = getSortedPlateValues(eGarageEntryStatus.Repaired);
                        break;
                    case eGarageEntryStatus.PaidFor:

                        o_ToReturn = getSortedPlateValues(eGarageEntryStatus.PaidFor);
                        break;
                    case eGarageEntryStatus.RepairInProgress:

                        o_ToReturn = getSortedPlateValues(eGarageEntryStatus.RepairInProgress);
                        break;

                }
            }
            
            
            
            return o_ToReturn.ToArray();
        }
       private  List<string> getSortedPlateValues(eGarageEntryStatus i_SortType)
        {
            List<string> o_ListToReturn=new List<string>();
            foreach (GarageEntry entry in m_VehicleDataBase.Values)
            {
                if (entry.Status == i_SortType)
                {
                    o_ListToReturn.Add(entry.r_Vehicle.GetLicansePlate());
                }
            }
            return o_ListToReturn;
        }
        public void ChangeVehicleState(eGarageEntryStatus i_State,string i_PlateNumber)
        {
            m_VehicleDataBase[i_PlateNumber].Status = i_State;
        }
       
        public void ChargeFuelVehicle(string i_PlateNumber, eFuelType i_FuelType,string i_AmountToFuel)
        {
            if(!DoesEntryAlreadyExist(i_PlateNumber))
            {
                throw new FormatException("vehicle not found");
            }
            if (!int.TryParse(i_AmountToFuel, out int fuelAmount))
            {
                throw new FormatException("Invalid format for fuel amount.");
            }
            Vehicle vehicleData = m_VehicleDataBase[i_PlateNumber].r_Vehicle;
            if (vehicleData is ICombustive electricVehicle)
            {
                electricVehicle.Fuel(i_FuelType,fuelAmount);
            }
            else
            {
                throw new FormatException("cannot fuel non fuel powered car");
            }
            int i = 9;
        }
        public void ChargeElectricVehicle(string i_PlateNumber, string i_MinutesToCharge)
        {
            if (!DoesEntryAlreadyExist(i_PlateNumber))
            {
                throw new FormatException("vehicle not found");
            }
            if (!int.TryParse(i_MinutesToCharge, out int chargeAmount))
            {
                throw new FormatException("Invalid format for charge amount.");
            }
            Vehicle data = m_VehicleDataBase[i_PlateNumber].r_Vehicle;
            if (data is IElectric electricVehicle)
            {
                electricVehicle.Charge(chargeAmount);
            }
            else
            {
                throw new FormatException("cannot charge non electricity powered car");
            }

        }
        public void ShowFullVehicleData(string i_PlateNumber)
        {

            string energyType=string.Empty;
            GarageEntry entry = m_VehicleDataBase[i_PlateNumber]; 
            List<string> wheelData=entry.r_Vehicle.GetAllWheelData();
            if(entry.r_Vehicle is ICombustive)
            {
              energyType= ((ICombustive)entry.r_Vehicle).GetFuelType().ToString();
            }
            if (entry.r_Vehicle is IElectric)
            {
                energyType = "electric";
            }
            energyType =string.Format("{0}|{1}",energyType,entry.r_Vehicle.EnergyPrecentage)  ;

            Console.WriteLine("License type:{0}, Model name:{1}, Owner name:{2}, Status:{3},Wheel data:{4},Energy type:{5}, Properties:{6}",
                     entry.r_Vehicle.GetLicansePlate(),
                     entry.r_Vehicle.GetModelName(),
                     entry.r_OwnerName,
                     entry.Status.ToString(),
                     string.Join(",", wheelData),
                     energyType,
                     string.Join(",", entry.r_Vehicle.GetPropertyValuesAsListOfStrings()));
        }
        
        public bool DoesEntryAlreadyExist(string i_PlateNumber)
        {
           return m_VehicleDataBase.ContainsKey(i_PlateNumber);
        }
        private string[] splitWheelDataSeparatedByComma(string data)
        {
            float pressure;
            string manufacturer;
            string[] o_SplitString;

            if(!data.Contains(","))
            {
                throw new FormatException("invalid format");

            }
             o_SplitString= data.Split(',');
            if(o_SplitString.Length > 2) 
            {
                throw new FormatException("too many instances of ','");
            }
            
            manufacturer = o_SplitString[0];
            if (!float.TryParse(o_SplitString[1], out pressure))
            {
                throw new FormatException("pressure should be float");
            }
            return o_SplitString;
        }
        public void SetCreatedWheelAttributes(eVehicleDataKeys i_DataKey,string i_WheelData)
        {
            if (m_VehicleBeingCreated == null)
            {
                throw new ArgumentException("cennot modify vehicle");
            }

            string[] splitString = splitWheelDataSeparatedByComma(i_WheelData);
            if(i_DataKey==eVehicleDataKeys.WheelData)
            {
                m_VehicleBeingCreated.AddWheel(splitString[0], float.Parse(splitString[1]));
            }
            if(i_DataKey ==eVehicleDataKeys.MultipleWheelData)
            {
                m_VehicleBeingCreated.InitAllWheels(splitString[0], float.Parse(splitString[1]));
            }
        }
        
        public void FillAirInEntry(string i_PlateNumber)
        {
            m_VehicleDataBase[i_PlateNumber].r_Vehicle.InflateAllTiresToMax();
        }
    }
}
