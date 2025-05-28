using CostumExceptions;
using BaseComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseVehicle
{
    public abstract class Vehicle
    {
        public readonly string m_modelName;
        public readonly string m_licansePlate;
        public readonly int m_maxNumOfWheels;
        public readonly float m_minWheelPressure;
        public readonly float m_maxWheelPressure;
        protected readonly BasePowertrain m_powerTrain;
        private List<Wheel> m_wheels;

        public float EnergyPrecentage
        {
            get
            {
                return m_powerTrain.GetEnergyPrecentage();
            }
        }

        public Vehicle(string i_modelName, string i_licansePlate,int i_maxWheelCount , float i_minWheelPressure, float i_maxWheelPressure, BasePowertrain i_powerTrain)
        {
            m_modelName = i_modelName;
            m_licansePlate = i_licansePlate;
            m_powerTrain = i_powerTrain;
            m_maxNumOfWheels = i_maxWheelCount;
            m_minWheelPressure = i_minWheelPressure;
            m_maxWheelPressure = i_maxWheelPressure;
            m_wheels = new List<Wheel>(i_maxWheelCount);
        }

        public void AddWheel(string i_manufacturer, float i_initialPressure)
        {
            if (m_wheels.Count >= m_maxNumOfWheels)
            {
                string messege = $"This vehicle already has the maximum of {m_maxNumOfWheels} wheels.";
                throw new ArgumentException(messege);
            }

            m_wheels.Add(new Wheel(i_manufacturer,m_maxWheelPressure,m_minWheelPressure,i_initialPressure));
        }

        public void InitAllWheels(string i_manufacturer, float i_initialPressure)
        {
            if (m_wheels.Count > 0)
                throw new ArgumentException("Wheels already initialised.");

            for (int i = 0; i < this.m_maxNumOfWheels; i++)
                this.AddWheel(i_manufacturer, i_initialPressure);
        }

        public void InflateAllTiresToMax()
        {

            if (m_wheels.Count == 0)
                throw new InvalidOperationException("No wheels have been added.");

            foreach (Wheel wheel in m_wheels)
            {
                float delta = m_maxWheelPressure - wheel.Pressure;
                if (delta > 0)
                    wheel.Inflate(delta);
            }
        }

        public abstract void SetAllPropertiesFromOrderdListOfStrings(List<string> i_PropertyValuesList);

        public abstract List<string> GetPropertyValuesAsListOfStrings();


        //* explenation
        //Again this is the only way to enshure when adding a new base Vehicle type, no changes will be made
        //it is code duplication, but this puts the responsebiliy of the developer who is adding the  vhicle type class.
        //we recognize it ( generic setters froms string) is bad practice,
        //but there was no other way to make "No code changes at all when adding a new type" as instructed.

        protected class Wheel
        {
            // Has to be tightly coupled since vehicle type dictates the pressure.
            public readonly string m_manufacturer;
            public readonly float m_maxPressure;
            public readonly float m_minPressure;
            public float Pressure { get; set; }

            public Wheel(string i_manufacturer, float i_maxPressure, float i_minPressure, float i_initalPressure)
            {
                m_manufacturer = i_manufacturer;
                m_maxPressure = i_maxPressure;
                m_minPressure = i_minPressure;
                Pressure = i_minPressure;
                this.Inflate(i_initalPressure);
            }

            public void Inflate(float i_addedPressure)
            {
                if (Pressure < 0)
                {
                    string messege = $"Error: cannot deflate tire using 'Inflate' method. actual pressure given to inflate : {i_addedPressure}";
                    throw new ArgumentException(messege);
                }

                float desiredPressure = this.Pressure + i_addedPressure;
                if (desiredPressure <= this.m_maxPressure && desiredPressure >= this.m_minPressure)
                {
                    this.Pressure = desiredPressure;
                }
                else
                {
                    string messege = $"Error: Wheel pressure range exceeded. range: from {m_minPressure} PSI to {m_maxPressure} PSI,  but was set to {desiredPressure}";
                    throw new ValueRangeException(messege, m_minPressure, m_maxPressure);
                }
            }
        }
    }
}


