using BaseComponents;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseVehicle;
using System.Globalization;

namespace Ex03.GarageLogic.VehicleTypes
{
    public class Car : BaseVehicle.Vehicle
    {
        //Given Consts
        private const int k_NumOfWheels = 5;
        private const float k_MaxWheelsPressure = 32f;
        private const float k_WheelMinPressure = 0f;
        public CarColor CarColor { get; set; }
        public int NumberOfDoors { get; set; }
        private const int k_NumberOfProperties = 2;
        //part of *
        private static readonly IReadOnlyList<string> sr_PropertyNames = new List<string>(k_NumberOfProperties)
        { "CarColor", "NumberOfDoors" }.AsReadOnly();
        //part of *
        private readonly IReadOnlyList<Type> sr_PropertyTypes = new List<Type>(k_NumberOfProperties)
        { typeof(Enum), typeof(int) }.AsReadOnly();

        public Car(string i_modelName, string i_licansePlate, BasePowertrain i_powerTrain)
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

            if (!Enum.TryParse<CarColor>(i_PropertyValuesList[0], ignoreCase: true, out var color) || !Enum.IsDefined(typeof(CarColor), color))
            {
                throw new FormatException(
                    $"\"{i_PropertyValuesList[0]}\" is not a valid Car Color value.");
            }

            if (!int.TryParse(i_PropertyValuesList[1], out int numOfDoors))
            {
                string messege = $"\"{i_PropertyValuesList[1]}\" is not a valid int expected a whole number";
                throw new FormatException(messege);
            }
            CarColor = color;
            NumberOfDoors = numOfDoors;
        }

        //part of *
        public override List<string> GetPropertyValuesAsListOfStrings()
        {
            List<string> o_ListOfValues;

            string asStringCarColor = CarColor.ToString();
            string asStringNumOfDoors = NumberOfDoors.ToString();

            o_ListOfValues = new List<string>() { asStringCarColor, asStringNumOfDoors };

            return o_ListOfValues;
        }
    }
}
