using CostumExceptions;
using BaseComponents;

namespace BaseVehicle
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicansePlate;
        private readonly int r_MaxNumOfWheels;
        private readonly float r_MinWheelPressure;
        private readonly float r_MaxWheelPressure;
        protected readonly BasePowertrain r_PowerTrain;
        private readonly List<Wheel> r_Wheels;

        public float EnergyPrecentage
        {
            get
            {
                return r_PowerTrain.GetEnergyPrecentage();
            }
        }

        public Vehicle(string i_modelName, string i_licansePlate,int i_maxWheelCount , float i_minWheelPressure, float i_maxWheelPressure, BasePowertrain i_powerTrain)
        {
            r_ModelName = i_modelName;
            r_LicansePlate = i_licansePlate;
            r_PowerTrain = i_powerTrain;
            r_MaxNumOfWheels = i_maxWheelCount;
            r_MinWheelPressure = i_minWheelPressure;
            r_MaxWheelPressure = i_maxWheelPressure;
            r_Wheels = new List<Wheel>(i_maxWheelCount);
        }

        public string GetModelName()
        {
            return r_ModelName;
        }

        public string GetLicansePlate()
        {
            return r_LicansePlate;
        }

        public int GetMaxNumOfWheels()
        {
            return r_MaxNumOfWheels;
        }

        public float GetMinWheelPressure()
        {
            return r_MinWheelPressure;
        }

        public float GetMaxWheelPressure()
        {
            return r_MaxWheelPressure;
        }

        public void AddWheel(string i_manufacturer, float i_initialPressure)
        {
            if (r_Wheels.Count >= r_MaxNumOfWheels)
            {
                string messege = $"This vehicle already has the maximum of {r_MaxNumOfWheels} wheels.";
                throw new ArgumentException(messege);
            }

            r_Wheels.Add(new Wheel(i_manufacturer,r_MaxWheelPressure,r_MinWheelPressure,i_initialPressure));
        }

        public void InitAllWheels(string i_manufacturer, float i_initialPressure)
        {
            if (r_Wheels.Count > 0)
                throw new ArgumentException("Wheels already initialised.");

            for (int i = 0; i < this.r_MaxNumOfWheels; i++)
                this.AddWheel(i_manufacturer, i_initialPressure);
        }

        public void InflateAllTiresToMax()
        {

            if (r_Wheels.Count == 0)
                throw new InvalidOperationException("No wheels have been added.");

            foreach (Wheel wheel in r_Wheels)
            {
                float delta = wheel.GetMaxPressure() - wheel.Pressure;
                if (delta > 0)
                    wheel.Inflate(delta);
            }
        }


        //* explenation
        //Again this is the only way to enshure when adding a new base Vehicle type, no changes will be made
        //it is code duplication, but this puts the responsebiliy of the developer who is adding the  vhicle type class.
        //we recognize it ( generic setters froms string) is bad practice,
        //but there was no other way to make "No code changes at all when adding a new type" as instructed.
        public abstract void SetAllPropertiesFromOrderdListOfStrings(List<string> i_PropertyValuesList);

        public abstract List<string> GetPropertyValuesAsListOfStrings();


        // Has to be tightly coupled since vehicle type dictates the pressure.
        // For now, it does not make sense for the wheels to be created outside of Vehicle
        protected class Wheel
        {

            private readonly string r_Manufacturer;
            private readonly float r_MaxPressure;
            private readonly float r_MinPressure;
            public float Pressure { get; set; }

            public Wheel(string i_manufacturer, float i_maxPressure, float i_minPressure, float i_initalPressure)
            {
                r_Manufacturer = i_manufacturer;
                r_MaxPressure = i_maxPressure;
                r_MinPressure = i_minPressure;
                Pressure = i_minPressure;
                this.Inflate(i_initalPressure);
            }

            public string GetManufacturer()
            {
                return r_Manufacturer;
            }

            public float GetMaxPressure()
            {
                return r_MaxPressure;
            }

            public float GetMinPressure()
            {
                return r_MinPressure;
            }

            public void Inflate(float i_addedPressure)
            {
                if (Pressure < 0)
                {
                    string messege = $"Error: cannot deflate tire using 'Inflate' method. actual pressure given to inflate : {i_addedPressure}";
                    throw new ArgumentException(messege);
                }

                float desiredPressure = this.Pressure + i_addedPressure;
                if (desiredPressure <= this.r_MaxPressure && desiredPressure >= this.r_MinPressure)
                {
                    this.Pressure = desiredPressure;
                }
                else
                {
                    string messege = $"Error: Wheel pressure range exceeded. range: from {r_MinPressure} PSI to {r_MaxPressure} PSI,  but was set to {desiredPressure}";
                    throw new ValueRangeException(messege, r_MinPressure, r_MaxPressure);
                }
            }
        }
    }
}


