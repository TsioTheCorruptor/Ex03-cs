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
        public readonly Vehicle r_Vehicle;
        public readonly string r_OwnerName;
        public readonly string r_OwnerPhoneNumber;
        public GarageEntryStatus Status {  get; set; }

        public GarageEntry(Vehicle i_vehicle, string i_ownerName, string i_ownerPhoneNumber) 
        { 
            r_Vehicle = i_vehicle;
            r_OwnerName = i_ownerName;
            r_OwnerPhoneNumber = i_ownerPhoneNumber;
            this.Status = GarageEntryStatus.RepairInProgress;
        }
    }
}
