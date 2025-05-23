using System;

namespace UI
{
    internal class Ui
    {
        private enum e_UserOptions { ReadVehicleDataFromFile, AddVehicle }

        private enum e_VehicleTypes
        {
            FuelCar,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        // Keeps the same name, but now it's dynamically generated from the enum
        private readonly string[] m_vehicleTypesToEnter = Enum.GetNames(typeof(e_VehicleTypes));

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

        public void AddVehicle()
        {
            bool validNameIndex = true;
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
                printVehicleTypesToChoose();
                validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);

                while (validNameIndex == false || vehicleNameIndex >= m_vehicleTypesToEnter.Length || vehicleNameIndex < 0)
                {
                    Console.WriteLine("Number must be between 0 to {0}", m_vehicleTypesToEnter.Length - 1);
                    printVehicleTypesToChoose();
                    validNameIndex = int.TryParse(Console.ReadLine(), out vehicleNameIndex);
                }

                e_VehicleTypes selectedType = (e_VehicleTypes)vehicleNameIndex;
                switch(selectedType)
                {
                    case e_VehicleTypes.ElectricCar:
                        break;
                    case e_VehicleTypes.ElectricMotorcycle:
                        break;
                    case e_VehicleTypes.FuelCar:
                        break;
                    case e_VehicleTypes.FuelMotorcycle:
                        break;
                    case e_VehicleTypes.Truck:
                        break;
                }

                // TODO: Use selectedType in further logic
            }
        }
    }
}