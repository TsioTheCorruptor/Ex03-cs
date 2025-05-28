using BaseComponents;
using BaseVehicle;
using Enums;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Ex03.GarageLogic.VehicleTypes
{
    public class BaseTruck: BaseVehicle.Vehicle
    {
        //Given Consts
        private const int k_NumOfWheels = 12;
        private const float k_MaxWheelsPressure = 27f;
        private const float k_WheelMinPressure = 0f;
        //Properties
        public bool IsCarryingHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }
        //explenation in *
        private const int k_NumberOfProperties = 2;
        //explenation in *
        private static readonly IReadOnlyList<string> sr_PropertyNames = new List<string>(k_NumberOfProperties) 
        { "IsCarryingHazardousMaterials", "CargoVolume" }.AsReadOnly();
        //explenation in *
        private readonly IReadOnlyList<Type> sr_PropertyTypes = new List<Type>(k_NumberOfProperties)
        { typeof(bool), typeof(float) }.AsReadOnly();
    public BaseTruck(string i_modelName, string i_licansePlate, BasePowertrain i_powerTrain)
            : base(i_modelName, i_licansePlate, k_NumOfWheels, k_WheelMinPressure, k_MaxWheelsPressure, i_powerTrain)
        {

        }

        public IReadOnlyList<string> GetPropertyNamesList()
        {
            return sr_PropertyNames;
        }

        public IReadOnlyList<Type> GetPropertyTypesList()
        {
            return sr_PropertyTypes;
        }
        //part of *
        public override void SetAllPropertiesFromOrderdListOfStrings(List<string> i_PropertyValuesList)
        {
            if (i_PropertyValuesList.Count != k_NumberOfProperties)
            {
                string messege = $"Error: Too many properties given: expected : {k_NumberOfProperties} got: {i_PropertyValuesList.Count}";
                throw new FormatException(messege);
            }

            if (!bool.TryParse(i_PropertyValuesList[0], out bool hazardous))
            {
                string messege = $"\"{i_PropertyValuesList[0]}\" is not a valid Boolean  expected 'true'/'false'";
                throw new FormatException(messege);
            }

            if (!float.TryParse(i_PropertyValuesList[1],NumberStyles.Float,CultureInfo.InvariantCulture,out float volume))
            {
                throw new FormatException(
                    $"\"{i_PropertyValuesList[1]}\" is not a valid floating-point number.");
            }
            IsCarryingHazardousMaterials = hazardous;
            CargoVolume = volume;
        }
        //part of *
        public override List<string> GetPropertyValuesAsListOfStrings()
        {
            List<string> o_ListOfValues;

            string asStringCargoVolume = CargoVolume.ToString();
            string asStringIsHazardus = IsCarryingHazardousMaterials.ToString();

            o_ListOfValues = new List<string>() { asStringIsHazardus, asStringIsHazardus };

            return o_ListOfValues;
        }
    }
}
