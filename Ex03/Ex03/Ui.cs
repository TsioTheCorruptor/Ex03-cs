using System;
using System.Xml.Linq;
using Ex03.GarageLogic;
using Enums;
namespace UI
{
    internal class Ui
    {
        private enum e_UserOptions { ReadVehicleDataFromFile, AddVehicle }
        private bool m_IsvehicleValid = true;
        private readonly string[] m_doorAmountsToEnter = { "2", "3", "4", "5" };
        GarageLogic m_Garage=new GarageLogic();
        private enum e_licenseType
        {
            A, A2, AB, B2
        }
        private enum e_FuelType
        {
            Octan95, Octan96, Octan98, Soler
        }
        private enum e_carColors
        {
            black,
            white,
            silver,
            yellow
        }
        private enum e_VehicleTypes
        {
            FuelCar,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }
        private enum e_VehicleDataKeys
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

        // Keeps the same name, but now it's dynamically generated from the enum
        private readonly string[] m_vehicleTypesToEnter = Enum.GetNames(typeof(e_VehicleTypes));
        private readonly string[] m_carColorsToEnter = Enum.GetNames(typeof(e_carColors));
        private readonly string[] m_licenseTypeToEnter = Enum.GetNames(typeof(e_licenseType));
        private readonly string[] m_FuelTypeToEnter = Enum.GetNames(typeof(e_FuelType));
        private readonly string[] m_userOptionsStrings = { "not yet", "Add car to garage" };

        private void optionSelect(e_UserOptions option)
        {
            switch (option)
            {
                case e_UserOptions.ReadVehicleDataFromFile:
                    // Add logic here
                    break;
                case e_UserOptions.AddVehicle:
                    AddVehicle();
                    break;
            }
        }

        private void printUserOptions()
        {
            for (int i = 0; i < m_userOptionsStrings.Length; i++)
            {
                Console.WriteLine("{0}-{1}", i, m_userOptionsStrings[i]);
            }
        }

        
        private void printStringArray(string[] i_strings,string i_openingText)
        {
            Console.WriteLine(i_openingText);
            for (int i = 0; i < i_strings.Length; i++)
            {
                Console.WriteLine("{0}|{1}", i, i_strings[i]);
            }
        }
        public void AddVehicle()
        {
            bool validNameIndex = true;
            m_IsvehicleValid = true;
            string? plateNumber;
            int vehicleNameIndex;
           
            Console.WriteLine("Enter license plate number:");
            plateNumber = Console.ReadLine();

            if (false) // TODO: check if already exists
            {
                // handle duplicate
            }
            else
            {
                printStringArray(m_vehicleTypesToEnter, "Enter your Vehicle type:");
                validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);

                while (validNameIndex == false || vehicleNameIndex >= m_vehicleTypesToEnter.Length || vehicleNameIndex < 0)
                {
                    Console.WriteLine("Number must be between 0 to {0}", m_vehicleTypesToEnter.Length - 1);
                    printStringArray(m_vehicleTypesToEnter, "Enter your Vehicle type:");
                    validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);
                }
                
                e_VehicleTypes selectedType = (e_VehicleTypes)vehicleNameIndex;
                m_Garage.CreateVehicle(m_vehicleTypesToEnter[vehicleNameIndex], plateNumber);
                collectCommonData();
                if(m_Garage.getCarEnergyType()=="Fuel")
                {
                    collectFuelData();
                }
                else if(m_Garage.getCarEnergyType()=="Electric")
                {
                    collectElectricData();
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
            getDataFromUserDirectly("Enter energy percentage:", Enums.e_VehicleDataKeys.EnergyPercentage);
            

            for(int i=0; i<m_Garage.GetAmountOfWheelsInCurrentCar(); i++)
            {
                getDataFromUserDirectly("Enter wheel manufacturer name:", Enums.e_VehicleDataKeys.WheelManufacturer);
                getDataFromUserDirectly(string.Format("Enter current air pressure in wheel {0}:", i+1),Enums.e_VehicleDataKeys.CurrentAirPressure);
            }
            
        }

        private void collectCarData()
        {
            
            getDataFromOptions(m_carColorsToEnter,"Enter car color index :" , Enums.e_VehicleDataKeys.Color);
            getDataFromOptions(m_doorAmountsToEnter,"Enter number of doors " , Enums.e_VehicleDataKeys.NumberOfDoors);
            
        }

        private void collectMotorcycleData()
        {
            
            getDataFromOptions(m_licenseTypeToEnter, "Enter license type index:", Enums.e_VehicleDataKeys.LicenseType);
            getDataFromUserDirectly("Enter engine capacity (cc):", Enums.e_VehicleDataKeys.EngineCapacity);

        }

        private void collectFuelData()
        {
            
            getDataFromOptions( m_FuelTypeToEnter,"Enter fuel type index :", Enums.e_VehicleDataKeys.FuelType);
            getDataFromUserDirectly("Enter current amount of fuel (liters):", Enums.e_VehicleDataKeys.CurrentFuelAmount);
            
        }

        private void collectElectricData()
        {
            
            getDataFromUserDirectly("Enter current battery time (hours):" ,Enums.e_VehicleDataKeys.CurrentBatteryTime);
        }

        private void collectTruckData()
        {

            getDataFromUserDirectly("Is carrying dangerous materials? (true/false):", Enums.e_VehicleDataKeys.IsCarryingDangerousMaterials);
           getDataFromUserDirectly("Enter cargo volume:", Enums.e_VehicleDataKeys.CargoVolume);
          
           
        }
        private void getDataFromOptions(string[] i_options,string i_entryText,Enums.e_VehicleDataKeys i_dataKey)
        {
            bool restartLoop=true;
            int data;
            
            while (restartLoop)
            {
                printStringArray(i_options, i_entryText);
                 int.TryParse(Console.ReadLine(),out data); 
                if(data>i_options.Length)
                {
                    continue;
                }
                
                try
                {
                    m_Garage.SetCreatedVehicleAttribute(i_dataKey, i_options[data]);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                restartLoop = false;
            }

        }
        private void getDataFromUserDirectly( string i_entryText, Enums.e_VehicleDataKeys i_dataKey)
        {
            bool restartLoop = true;
            string? data;

            while (restartLoop)
            {
                Console.WriteLine(i_entryText);
                data = Console.ReadLine();
                try
                {
                    if(data!=null)
                    {
                        m_Garage.SetCreatedVehicleAttribute(i_dataKey, data);
                    }
                    else
                    {
                        continue;
                    }
                    
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                restartLoop = false;
            }

        }

    }
}