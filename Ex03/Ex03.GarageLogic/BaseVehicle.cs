using CostumExceptions;
using BaseComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    public abstract class BaseVehicle
    {
        public string OwnerName { get; set;}
        public string OwnerPhone { get; set;}

        protected BasePowertrain m_powerTrain;
        protected float m_energyPrecentage;
        

        protected List<Wheel> m_wheels = null;

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
                Pressure = i_initalPressure;
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


