using BaseVehicle;                       
using Ex03.GarageLogic.Combustive;
using Ex03.GarageLogic.Electric;
using System.ComponentModel;

namespace Ex03.GarageLogic.IO
{
    public static class VehicleFileImporter
    {
        public static IReadOnlyList<GarageEntry> Import(string i_FilePath)
        {
            if (!File.Exists(i_FilePath))
            {
                throw new FileNotFoundException($"Input file '{i_FilePath}' was not found.");
            }

            var entries = new List<GarageEntry>();

            string[] lines = File.ReadAllLines(i_FilePath);

            foreach (string rawLine in lines)
            {
                
                if (string.IsNullOrWhiteSpace(rawLine) || rawLine.TrimStart().StartsWith("#"))
                {
                    continue;
                }

                string[] parts = rawLine.Split(',', StringSplitOptions.TrimEntries);
                if (parts.Length < 8)
                {
                    throw new FormatException($"Invalid line – expected 8 comma‑separated fields but got {parts.Length}: '{rawLine}'.");
                }

                string ownerName = parts[0];
                string ownerPhone = parts[1];
                string vehicleTypeString = parts[2];
                string licenceId = parts[3];
                string modelName = parts[4];
                string wheelManufacturer = parts[5];
                if (!float.TryParse(parts[6], out float wheelPressure))
                {
                    throw new FormatException($"Wheel pressure is not a valid float: '{parts[6]}' in line '{rawLine}'.");
                }
                if (!float.TryParse(parts[7], out float energyAmount))
                {
                    throw new FormatException($"Energy amount is not a valid float: '{parts[7]}' in line '{rawLine}'.");
                }

                Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleTypeString, licenceId, modelName);
                
                if (vehicle == null)
                {
                    throw new ArgumentException($"Vehicle type '{vehicleTypeString}' is not supported.");
                }

                vehicle.InitAllWheels(wheelManufacturer, wheelPressure);


                switch (vehicle)
                {
                    case IElectric eVehicle:
                        eVehicle.Charge(energyAmount);
                        break;
                    case ICombustive cVehicle:
                        cVehicle.Fuel(cVehicle.GetFuelType(), energyAmount);
                        break;
                }
                List<string> specialPropertyList = parts.Skip(8).ToList();

                vehicle.SetAllPropertiesFromOrderdListOfStrings(specialPropertyList);
                
                
                entries.Add(new GarageEntry(vehicle, ownerName, ownerPhone));
            }

            return entries;
        }
    }
}
