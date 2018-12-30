using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model.Abstract;

namespace App.Model.Entity
{
    [Table("ItemMasterInventorys")]
    public class ItemMasterInventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IMISiteID { get; set; }
        public int ItemMasterID { get; set; }
        public double IMIQtyOnHand { get; set; }
        public double IMIQtyAllocated { get; set; }

        [ForeignKey("IMISiteID")]
        public virtual Site site { get; set; }

        [ForeignKey("ItemMasterID")]
        public virtual ItemMaster ItemMaster { get; set; }
    }
}
