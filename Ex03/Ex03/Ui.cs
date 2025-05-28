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
        private enum e_UserOptions { ReadVehicleDataFromFile, AddVehicle,GetLicensePlateList,ChangeVehicleState,
                                     FillAirInTires,ChargeFuelVehicle,ChargeElectricVehicle,ShowFullVehicleData,Quit }
        GarageLogic m_Garage=new GarageLogic();
       
        // Keeps the same name, but now it's dynamically generated from the enum
        private readonly string[] m_VehicleTypesToEnter = Enum.GetNames(typeof(e_VehicleTypes));
        private readonly string[] m_VehicleStatesToEnter = Enum.GetNames(typeof(GarageEntryStatus));
        private readonly string[] m_WheelInputOptionsToEnter = {"enter pressure and manufacturer for all wheels","enter  individually" };
        private readonly string[] m_UserOptionsStrings = { " Read Vehicle Data From File", "Add car to garage", 
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
            e_UserOptions option;
            while (true)
            {
                option = (e_UserOptions)getIndexFromOptions(m_UserOptionsStrings, "Enter Your Option");
                switch (option)
                {
                    case e_UserOptions.ReadVehicleDataFromFile:
                        try
                        {
                            m_Garage.ReadVehicleDataFromFile();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;
                    case e_UserOptions.AddVehicle:
                        AddVehicle();
                        break;
                    case e_UserOptions.GetLicensePlateList:

                        break;
                    case e_UserOptions.ChangeVehicleState:
                        string? plateNumber;
                        int stateIndex;
                        plateNumber=Console.ReadLine();
                       stateIndex= getIndexFromOptions(m_VehicleStatesToEnter,"select new state");
                        m_Garage.ChangeVehicleState((GarageEntryStatus)stateIndex, plateNumber);
                        break;
                    case e_UserOptions.FillAirInTires:

                        break;
                    case e_UserOptions.ChargeFuelVehicle:

                        break;
                    case e_UserOptions.ChargeElectricVehicle:

                        break;
                    case e_UserOptions.ShowFullVehicleData:

                        break;
                    case e_UserOptions.Quit:
                        quit = true;
                        break;
                }
                if (quit)
                {
                    break;
                }


            }

        }

        private void printUserOptions()
        {
            for (int i = 0; i < m_UserOptionsStrings.Length; i++)
            {
                Console.WriteLine("{0}-{1}", i, m_UserOptionsStrings[i]);
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
        public void AddVehicle()
        {
            bool validNameIndex = true;
            string? plateNumber;
            string? modelName;
            int vehicleNameIndex;
            bool entryExists;
            Console.WriteLine("Enter license plate number:");
            plateNumber = Console.ReadLine();
           
            entryExists=m_Garage.DoesEntryAlreadyExist(plateNumber);
            if (entryExists) // TODO: check if already exists
            {
                Console.WriteLine("Entry already exists , Changing state to |Repair in Progress|");
                m_Garage.ChangeVehicleState(GarageEntryStatus.RepairInProgress,plateNumber);
            }
            else
            {
                Console.WriteLine("Enter model name:");
                modelName = Console.ReadLine();
                
                printStringArray(m_VehicleTypesToEnter, "Enter your Vehicle type:");
                validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);

                while (validNameIndex == false || vehicleNameIndex >= m_VehicleTypesToEnter.Length || vehicleNameIndex < 0)
                {
                    Console.WriteLine("Number must be between 0 to {0}", m_VehicleTypesToEnter.Length - 1);
                    printStringArray(m_VehicleTypesToEnter, "Enter your Vehicle type:");
                    validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);
                }

                e_VehicleTypes selectedType = (e_VehicleTypes)vehicleNameIndex;
                m_Garage.CreateTempVehicle(plateNumber,m_VehicleTypesToEnter[vehicleNameIndex] ,modelName);
                collectCommonData();
                if (m_Garage.getCarEnergyType() == GarageLogic.AvailableEnergyTypes.fuel) //make it enum
                {
                    collectFuelData();
                }
                else
                {
                    if (m_Garage.getCarEnergyType() == GarageLogic.AvailableEnergyTypes.electric)
                    {
                        collectElectricData();
                    }
                }
                
                Enums.e_VehicleDataKeys[] PropertyDataArr = m_Garage.GetPropertiesInfo();
                foreach (Enums.e_VehicleDataKeys PropertyData in PropertyDataArr)
                {
                    getDataFromUserDirectly(PropertyData.ToString(), PropertyData);
                }

               
                // TODO: send to engine to create vehicle with data

            }
        }
        private void collectCommonData()
        {
           
            int wheelInputOption;
            
            

            wheelInputOption= getIndexFromOptions(m_WheelInputOptionsToEnter, "enter wheel input method");

            if (wheelInputOption == 1)
            {
                for (int i = 0; i < m_Garage.GetAmountOfWheelsInVehicleBeingCreated(); i++)
                {
                    getDataFromUserDirectly("Enter wheel manufacturer name and air pressure in format- |name,pressure|:", e_VehicleDataKeys.WheelData);

                }

            }
            else
            {
                if (wheelInputOption == 0)
                {
                    getDataFromUserDirectly("Enter wheel manufacturer name and air pressure in format- |name,pressure|:", e_VehicleDataKeys.MultipleWheelData);
                }
            }
                
        }

       

        private void collectFuelData()
        {
            
            
            getDataFromUserDirectly("Enter current amount of fuel (liters):", Enums.e_VehicleDataKeys.CurrentFuelAmount);
            
        }

        private void collectElectricData()
        {
            
            getDataFromUserDirectly("Enter current battery time (hours):" ,Enums.e_VehicleDataKeys.CurrentBatteryTime);
        }


        private void getDataFromOptions(string[] i_Options, string i_EntryText, Enums.e_VehicleDataKeys i_DataKey)
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
        private void getDataFromUserDirectly(string i_EntryText, Enums.e_VehicleDataKeys i_DataKey)
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
            
            int data;
            bool isValid=false;
            while (true)
            {
                printStringArray(i_Options, i_EntryText);
                isValid = int.TryParse(Console.ReadLine(), out data);
                if (data > i_Options.Length || !isValid)
                {
                    continue;
                }

                return data;
            }

        }

    }
}