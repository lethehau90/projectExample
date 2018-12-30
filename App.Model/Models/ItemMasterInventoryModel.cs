using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Models
{
    public class ItemMasterInventoryModel
    {
        public int Id { get; set; }
        public int IMISiteID { get; set; }
        public int ItemMasterID { get; set; }
        public double IMIQtyOnHand { get; set; }
        public double IMIQtyAllocated { get; set; }
    }

    public class listMasterJoinSiteModel {
        public int Id { get; set; }
        public int IMISiteID { get; set; }
        public string NameSite { get; set; }
        public int ItemMasterID { get; set; }
        public double IMIQtyOnHand { get; set; }
        public double IMIQtyAllocated { get; set; }
    }
}
