using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehicle_ShowRoom_Manager_System.Models
{
    public class CollectionDataModel
    {
        public List<Vehicle> Vehicles { get; set; }
        public List<VehicleImg> VehicleImg { get; set; }
    }
}