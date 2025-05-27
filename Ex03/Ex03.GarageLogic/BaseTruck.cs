using BaseComponents;
using BaseVehicle;

namespace Ex03.GarageLogic.VehicleTypes
{
    public class BaseTruck: BaseVehicle.Vehicle
    {
        //Given Consts
        private const int k_numOfWheels = 12;
        private const float k_maxWheelsPressure = 27f;
        private const float k_WheelMinPressure = 0f;
        public bool IsCarryingHazardousMaterials;
        public float CargoVolume;

        public BaseTruck(string i_modelName, string i_licansePlate, BasePowertrain i_powerTrain)
            : base(i_modelName, i_licansePlate, k_numOfWheels, k_WheelMinPressure, k_maxWheelsPressure, i_powerTrain)
        {

        }
    }
}
