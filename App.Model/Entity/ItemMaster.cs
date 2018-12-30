using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Entity
{
    [Table("ItemMasters")]
    public class ItemMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IMPack { get; set; }
        public string IMDescription { get; set; }
        public string IMImageData { get; set; }
        public bool IMIsHazardousMaterial { get; set; }
        public DateTime IMExpirationDate { get; set; }
        public double IMUnitPrice { get; set; }
        public int IMWidth { get; set; }
        public int IMLength { get; set; }
        public int IMHeight { get; set; }
        public bool IMIsPrePack { get; set; }
        public string IMPrePackStyle { get; set; }
        public string IMCostCenterCode { get; set; }
        public virtual ICollection<ItemMasterInventory> ItemMasterInventorys { get; set; }
    }
}
