using BaseVehicle;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageEntry
    {
        public readonly Vehicle m_vehicle;
        public readonly string m_ownerName;
        public readonly string m_ownerPhoneNumber;
        public GarageEntryStatus Status {  get; set; }

        public GarageEntry(Vehicle i_vehicle, string i_ownerName, string i_ownerPhoneNumber) 
        { 
            m_vehicle = i_vehicle;
            m_ownerName = i_ownerName;
            m_ownerPhoneNumber = i_ownerPhoneNumber;
            this.Status = GarageEntryStatus.RepairInProgress;
        }
    }
}
