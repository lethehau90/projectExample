using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model.Entity;
using App.Model.Models;

namespace App.Service.IService
{
    public interface IItemMasterInventoryService
    {
        void Add(ItemMasterInventoryModel itemMasterInventoryModel);
        void Update(ItemMasterInventoryModel itemMasterInventoryModel);
        IEnumerable<listMasterJoinSiteModel> GetMuti(int Id);
        void Save();
    }
}
