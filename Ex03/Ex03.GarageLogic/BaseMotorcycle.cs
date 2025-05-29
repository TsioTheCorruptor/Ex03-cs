using BaseVehicle;
using Enums;
using BaseComponents;

namespace Ex03.GarageLogic.BaseVehicleTypes
{
    public class BaseMotorcycle: Vehicle
    {
        //Given Consts
        private const int k_NumOfWheels = 2;
        private const float k_MaxWheelsPressure = 30f;
        private const float k_WheelMinPressure = 0f;
        public eMotorcycleLicenseType LicenceType { get; set; }
        public int EngineVolume { get; set; }

        //part of *
        private const int k_NumberOfProperties = 2;
        //part of *
        private static readonly IReadOnlyList<string> sr_PropertyNames = new List<string>(k_NumberOfProperties)
        { "LicenseType", "EngineVolume" }.AsReadOnly();
        //part of *
        private readonly IReadOnlyList<string> sr_PropertyTypes = new List<string>(k_NumberOfProperties)
        { "name", "int" }.AsReadOnly();

        public BaseMotorcycle (string i_modelName, string i_licansePlate, BasePowertrain i_powerTrain)
            : base(i_modelName, i_licansePlate, k_NumOfWheels, k_WheelMinPressure, k_MaxWheelsPressure, i_powerTrain)
        {

        }

        //part of *
        override public IReadOnlyList<string> GetPropertyNamesList()
        {
            return sr_PropertyNames;
        }

        //part of *
       override public IReadOnlyList<string> GetPropertyTypesList()
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

            if (!Enum.TryParse<eMotorcycleLicenseType>(i_PropertyValuesList[0], ignoreCase: true, out var licenseType) 
                || !Enum.IsDefined(typeof(eMotorcycleLicenseType), licenseType))
            {
                throw new FormatException($"\"{i_PropertyValuesList[0]}\" is not a valid Motorcycle License Type value.");
            }

            if (!int.TryParse(i_PropertyValuesList[1], out int engineVolume))
            {
                string messege = $"\"{i_PropertyValuesList[1]}\" is not a valid int expected a whole number";
                throw new FormatException(messege);
            }
            this.EngineVolume = engineVolume;
            this.LicenceType = licenseType;
        }

        //part of *
        public override List<string> GetPropertyValuesAsListOfStrings()
        {
            List<string> o_ListOfValues;

            string asStringLicenceType = LicenceType.ToString();
            string asStringEngineVolume = EngineVolume.ToString();

            o_ListOfValues = new List<string>() { asStringLicenceType, asStringEngineVolume };

            return o_ListOfValues;
        }
    }
}
