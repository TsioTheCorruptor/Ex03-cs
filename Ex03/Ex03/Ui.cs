using System;
using System.Xml.Linq;
using Ex03.GarageLogic;
using Enums;
using CostumExceptions;
using Ex03.GarageLogic.IO;
namespace UI
{
    internal class Ui
    {
        private enum eUserOptions { ReadVehicleDataFromFile, AddVehicle,GetLicensePlateList,ChangeVehicleState,
                                     FillAirInTires,ChargeFuelVehicle,ChargeElectricVehicle,ShowFullVehicleData,Quit }
        GarageLogic m_Garage=new GarageLogic();

        // Keeps the same name, but now it's dynamically generated from the enum
         private readonly string[] r_EntriesForFuel = { "enter plate number:", "enter fuel amount:" };
        private readonly string[] r_EntriesForOwner = { "owner name:", "phone number:" };
        private readonly string[] r_EntriesForElectric = { "enter plate number:", "enter charge amount in minutes:" };
        private readonly string[] r_VehicleTypesToEnter = Enum.GetNames(typeof(eVehicleTypes));
        private readonly string[] r_VehicleStatesToEnter = Enum.GetNames(typeof(eGarageEntryStatus));
        private readonly string[] r_FuelTypeToEnter= Enum.GetNames(typeof(eFuelType));
        private readonly string[] r_SortChoicesToEnter = Enum.GetNames(typeof(eSortOptions));
        private readonly string[] r_WheelInputOptionsToEnter = {"enter pressure and manufacturer for all wheels","enter  individually" };
        private readonly string[] r_SortOptionsToEnter = Enum.GetNames(typeof(eGarageEntryStatus));
        private readonly string[] r_UserOptionsStrings = { " Read Vehicle Data From File", "Add car to garage", 
                                                           "Get License Plate List", "Change Vehicle State",
                                                           "Fill Air In Tires", "Charge Fuel Vehicle",
                                                           "Charge Electric Vehicle",
                                                           "ShowFullVehicleData","Quit" };
        public void Run()
        {
            optionSelect();
        }
        private void optionSelect()
        {
            bool quit = false;
            eUserOptions option;
            while (true)
            {
                try
                {
                    option = (eUserOptions)getIndexFromOptions(r_UserOptionsStrings, "Enter Your Option");
                    switch (option)
                    {
                        case eUserOptions.ReadVehicleDataFromFile:
                            
                                m_Garage.ReadVehicleDataFromFile();
                            
                           
                            break;
                        case eUserOptions.AddVehicle:
                            addVehicle();
                            break;
                        case eUserOptions.GetLicensePlateList:
                            int sort = getIndexFromOptions(r_SortChoicesToEnter, "would you like to sort?:");
                            int sortOptions = 0;
                            if ((eSortOptions)sort == eSortOptions.Yes)
                            {
                                sortOptions = getIndexFromOptions(r_SortOptionsToEnter, "enter sort option:");
                            }
                            
                            string[] licensePlatenumberList = m_Garage.GetLicensePlateList((eGarageEntryStatus)sortOptions,(eSortOptions)sortOptions);
                            foreach (string number in licensePlatenumberList)
                            {
                                Console.WriteLine(number);
                            }
                            break;
                        case eUserOptions.ChangeVehicleState:
                            string? plateNumber=getLicensePlateNumber();
                            int stateIndex;
                            
                            stateIndex = getIndexFromOptions(r_VehicleStatesToEnter, "select new state");
                            m_Garage.ChangeVehicleState((eGarageEntryStatus)stateIndex, plateNumber);
                            break;
                        case eUserOptions.FillAirInTires:
                            string[] entriesForFillAir = { "enter plate number" };
                            List<string> airFillData = getStringsFromUser(entriesForFillAir);
                            m_Garage.FillAirInEntry(airFillData[0]);
                            break;
                        case eUserOptions.ChargeFuelVehicle:

                            
                            List<string> fuelData = getStringsFromUser(r_EntriesForFuel);
                            int fuelTypeIndex = getIndexFromOptions(r_FuelTypeToEnter, "enter fuel type");

                            m_Garage.ChargeFuelVehicle(fuelData[0], (eFuelType)fuelTypeIndex, fuelData[1]);


                            break;
                        case eUserOptions.ChargeElectricVehicle:
                            string[] entriesForElectric = { "enter plate number", "enter charge amount in minutes" };
                            List<string> ElectricData = getStringsFromUser(entriesForElectric);

                            m_Garage.ChargeElectricVehicle(ElectricData[0], ElectricData[1]);

                            break;
                        case eUserOptions.ShowFullVehicleData:
                            string dataToSend = getLicensePlateNumber();
                            m_Garage.ShowFullVehicleData(dataToSend);
                            break;
                        case eUserOptions.Quit:
                            quit = true;
                            break;
                    }
                    if (quit)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                


            }

        }
        private string getLicensePlateNumber()
        {
            string? plateNumber;
           
            Console.WriteLine("enter plate number");
            plateNumber = Console.ReadLine();
            if (plateNumber == null)
            {
                throw new ArgumentException("enter valid string");
            }
            return plateNumber;
        }
        private void printUserOptions()
        {
            for (int i = 0; i < r_UserOptionsStrings.Length; i++)
            {
                Console.WriteLine("{0}-{1}", i, r_UserOptionsStrings[i]);
            }
        }

        
        private void printStringArray(string[] i_Strings,string i_OpeningText)
        {
            Console.WriteLine(i_OpeningText);
            for (int i = 0; i < i_Strings.Length; i++)
            {
                Console.WriteLine("{0}|{1}", i, i_Strings[i]);
            }
        }
        private void addVehicle()
        {
            bool validNameIndex = true;
            string? plateNumber=getLicensePlateNumber();
            string? modelName;
            int vehicleNameIndex;
            bool entryExists;
            
           
            entryExists=m_Garage.DoesEntryAlreadyExist(plateNumber);
            if (entryExists) // TODO: check if already exists
            {
                Console.WriteLine("Entry already exists , Changing state to |Repair in Progress|");
                m_Garage.ChangeVehicleState(eGarageEntryStatus.RepairInProgress, plateNumber);
            }
            else
            {
                Console.WriteLine("Enter model name:");
                modelName = Console.ReadLine();

                printStringArray(r_VehicleTypesToEnter, "Enter your Vehicle type:");
                validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);

                while (validNameIndex == false || vehicleNameIndex >= r_VehicleTypesToEnter.Length || vehicleNameIndex < 0)
                {
                    Console.WriteLine("Number must be between 0 to {0}", r_VehicleTypesToEnter.Length - 1);
                    printStringArray(r_VehicleTypesToEnter, "Enter your Vehicle type:");
                    validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);
                }

                eVehicleTypes selectedType = (eVehicleTypes)vehicleNameIndex;
                m_Garage.CreateTempVehicle(plateNumber, r_VehicleTypesToEnter[vehicleNameIndex], modelName);
                collectCommonData();
                if (m_Garage.getCarEnergyType() == GarageLogic.eAvailableEnergyTypes.Fuel) //make it enum
                {
                    collectFuelData();
                }
                else
                {
                    if (m_Garage.getCarEnergyType() == GarageLogic.eAvailableEnergyTypes.Electric)
                    {
                        collectElectricData();
                    }
                }
                setVehiclePropertiesAndAddEntry();
                
                // TODO: send to engine to create vehicle with data

            }
        }
       private void setVehiclePropertiesAndAddEntry()
        {
           string phoneNumber;
            string ownerName;

            bool resetLoop = true;
            while (resetLoop)
            {
                try
                {
                    IReadOnlyList<string> PropertyDataList = m_Garage.GetPropertiesInfo();

                    List<string> propertyStrings = getStringsFromUser(PropertyDataList.ToArray());
                    m_Garage.SetPropertiesInfo(propertyStrings);
                    List<string> ownerData = getStringsFromUser(r_EntriesForOwner);
                    ownerName = ownerData[0];
                    phoneNumber = ownerData[1];

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                m_Garage.CreateEntryFromTempVehicle(phoneNumber,ownerName );
                resetLoop = false;

            }
            
        }
        private void collectCommonData()
        {
           
            int wheelInputOption;
            
            

            wheelInputOption= getIndexFromOptions(r_WheelInputOptionsToEnter, "enter wheel input method");

            if (wheelInputOption == 1)
            {
                for (int i = 0; i < m_Garage.GetAmountOfWheelsInVehicleBeingCreated(); i++)
                {
                    getDataFromUserDirectly("Enter wheel manufacturer name and air pressure in format- |name,pressure|:", eVehicleDataKeys.WheelData);

                }

            }
            else
            {
                if (wheelInputOption == 0)
                {
                    getDataFromUserDirectly("Enter wheel manufacturer name and air pressure in format- |name,pressure|:", eVehicleDataKeys.MultipleWheelData);
                }
            }
                
        }

       

        private void collectFuelData()
        {
            
            
            getDataFromUserDirectly("Enter current amount of fuel (liters):", Enums.eVehicleDataKeys.CurrentFuelAmount);
            
        }

        private void collectElectricData()
        {
            
            getDataFromUserDirectly("Enter current battery time (hours):" ,Enums.eVehicleDataKeys.CurrentBatteryTime);
        }


        private void getDataFromOptions(string[] i_Options, string i_EntryText, Enums.eVehicleDataKeys i_DataKey)
        {
            bool restartLoop = true;
            int data;
            bool isValid = false;
            while (restartLoop)
            {
                printStringArray(i_Options, i_EntryText);
                isValid = int.TryParse(Console.ReadLine(), out data);
                if (data > i_Options.Length)
                {
                    continue;
                }

                try
                {
                    m_Garage.SetCreatedVehicleAttribute(i_DataKey, i_Options[data]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                restartLoop = false;
            }

        }
        private void getDataFromUserDirectly(string i_EntryText, Enums.eVehicleDataKeys i_DataKey)
        {
            bool restartLoop = true;
            string? data;

            while (restartLoop)
            {
                Console.WriteLine(i_EntryText);
                data = Console.ReadLine();
                try
                {
                    if (data != null)
                    {
                        m_Garage.SetCreatedVehicleAttribute(i_DataKey, data);
                    }
                    else
                    {
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                

                restartLoop = false;
            }

        }
        private int getIndexFromOptions(string[] i_Options, string i_EntryText)
        {
            
            int o_data;
            bool isValid=false;
            while (true)
            {
                printStringArray(i_Options, i_EntryText);
                isValid = int.TryParse(Console.ReadLine(), out o_data);
                if (o_data > i_Options.Length || !isValid)
                {
                    continue;
                }

                return o_data;
            }

        }
        List<string> getStringsFromUser(string[] i_EntryTexts)
        {
            string? input;
            List<string> o_strings=new List<string>();

            foreach(string entryText in i_EntryTexts )
            {
                Console.Write(entryText);
                input= Console.ReadLine();
                if (input == null)
                {
                    throw new FormatException("string not entered");
                }
                else
                {
                    o_strings.Add(input);
                }
                
            }
            return o_strings;
        }
    }
}