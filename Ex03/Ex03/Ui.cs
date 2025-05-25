using System;
using System.Xml.Linq;

namespace UI
{
    internal class Ui
    {
        private enum e_UserOptions { ReadVehicleDataFromFile, AddVehicle }
        private bool m_IsvehicleValid = true;
        private readonly string[] m_doorAmountsToEnter = { "2", "3", "4", "5" };
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
            CargoVolume
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

        private void printVehicleTypesToChoose()
        {
            Console.WriteLine("Enter your Vehicle type:");
            for (int i = 0; i < m_vehicleTypesToEnter.Length; i++)
            {
                Console.WriteLine("{0}|{1}", i, m_vehicleTypesToEnter[i]);
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
            Dictionary<e_VehicleDataKeys, object> vehicleData = new Dictionary<e_VehicleDataKeys, object>();
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
                switch(selectedType)
                {
                    case e_VehicleTypes.FuelCar:
                        collectCarData(vehicleData);
                        collectFuelData(vehicleData);
                        break;
                    case e_VehicleTypes.ElectricCar:
                        collectCarData(vehicleData);
                        collectElectricData(vehicleData);
                        break;
                    case e_VehicleTypes.FuelMotorcycle:
                        collectMotorcycleData(vehicleData);
                        collectFuelData(vehicleData);
                        break;
                    case e_VehicleTypes.ElectricMotorcycle:
                        collectMotorcycleData(vehicleData);
                        collectElectricData(vehicleData);
                        break;
                    case e_VehicleTypes.Truck:
                        collectTruckData(vehicleData);
                        break;
                }

                // TODO: send to engine to create vehicle with data
                vehicleData.Clear();
            }
        }
        private void collectCommonData(Dictionary<e_VehicleDataKeys, object> data)
        {
            string? manufacturernName;
            float airPressure;

            Console.WriteLine("Enter wheel manufacturer name:");
           manufacturernName = Console.ReadLine();
            if(manufacturernName==null)
            {
                m_IsvehicleValid = false;
            }
            else
            {
                data[e_VehicleDataKeys.WheelManufacturer] = manufacturernName;
            }


            Console.WriteLine("Enter current air pressure in wheels:");
            
              m_IsvehicleValid = float.TryParse(Console.ReadLine(),out airPressure);
            
            data[e_VehicleDataKeys.CurrentAirPressure]=airPressure;
        }

        private void collectCarData(Dictionary<e_VehicleDataKeys, object> data)
        {
            int colorIndex;
            int doorAmount;
            printStringArray(m_carColorsToEnter, "Enter car color index :");
             m_IsvehicleValid=int.TryParse(Console.ReadLine(),out colorIndex) ;
            printStringArray(m_doorAmountsToEnter, "Enter number of doors ");
            m_IsvehicleValid = int.TryParse(Console.ReadLine(), out doorAmount);   
            data[e_VehicleDataKeys.NumberOfDoors] = doorAmount;
            data[e_VehicleDataKeys.Color] = m_carColorsToEnter[colorIndex];
        }

        private void collectMotorcycleData(Dictionary<e_VehicleDataKeys, object> data)
        {
            string? licenseType;
            int engineCapacity;
           printStringArray(m_licenseTypeToEnter,"Enter license type index:");
            licenseType= Console.ReadLine();
            if (licenseType == null)
            {
                m_IsvehicleValid = false;
            }
            else
            {
                data[e_VehicleDataKeys.LicenseType] = licenseType;
            }
            

            Console.WriteLine("Enter engine capacity (cc):");
             m_IsvehicleValid= int.TryParse(Console.ReadLine(),out engineCapacity);
            data[e_VehicleDataKeys.EngineCapacity]= engineCapacity;
        }

        private void collectFuelData(Dictionary<e_VehicleDataKeys, object> data)
        {
            int fuelTypeIndex;
            float fuelAmount;
            printStringArray(m_FuelTypeToEnter, "Enter fuel type index :");
            m_IsvehicleValid = int.TryParse(Console.ReadLine(), out fuelTypeIndex);

            Console.WriteLine("Enter current amount of fuel (liters):");
           m_IsvehicleValid= float.TryParse(Console.ReadLine(),out fuelAmount);
            data[e_VehicleDataKeys.CurrentFuelAmount] = fuelAmount;
            data[e_VehicleDataKeys.FuelType] = m_FuelTypeToEnter[fuelTypeIndex];
        }

        private void collectElectricData(Dictionary<e_VehicleDataKeys, object> data)
        {
            int time;
            Console.WriteLine("Enter current battery time (hours):");
           m_IsvehicleValid= int.TryParse(Console.ReadLine(), out time);
            data[e_VehicleDataKeys.CurrentBatteryTime] = time;
        }

        private void collectTruckData(Dictionary<e_VehicleDataKeys, object> data)
        {
            bool isCarrying;
            float Volume;
            Console.WriteLine("Is carrying dangerous materials? (true/false):");
            m_IsvehicleValid = bool.TryParse(Console.ReadLine(), out isCarrying);
            data[e_VehicleDataKeys.IsCarryingDangerousMaterials] = isCarrying;
            Console.WriteLine("Enter cargo volume:");
            m_IsvehicleValid=float.TryParse(Console.ReadLine(),out Volume);
            data[e_VehicleDataKeys.CargoVolume] = Volume;
        }
        
    }
}