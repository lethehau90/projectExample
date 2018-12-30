using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Entity
{
    [Table("Sites")]
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemMasterInventory> ItemMasterInventorys { get; set; }
    }
}
