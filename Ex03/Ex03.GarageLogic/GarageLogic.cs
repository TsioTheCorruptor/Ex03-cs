using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Vehicle;
namespace Ex03.GarageLogic
{
    public class GarageLogic
    {
        Dictionary<string,BaseVehicle> m_vehicleDataBase =new Dictionary<string,BaseVehicle>();
       public void SetCreatedVehicleAttribute(e_VehicleDataKeys key, string value)
        {

        }
        public int GetAmountOfWheelsInCurrentCar()
        {
            return 1;
        }
        public void CreateTempVehicle(string i_licenseNumber,string i_VehicleType)
        {

        }
       public string getCarEnergyType()
        {
            return "Fuel";
        }
        public e_VehicleDataKeys[] GetPropertiesInfo()
        {
            return new e_VehicleDataKeys[] { e_VehicleDataKeys.IsCarryingDangerousMaterials,e_VehicleDataKeys.Color };
        }
        
    }
}
