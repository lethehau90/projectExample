using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model.Entity;
using App.Model.Models;

namespace App.Service.IService
{
    public interface IItemMasterService
    {
        void Add(ItemMasterModel itemMasterModel);
        void Update(ItemMasterModel itemMasterModel);
        ItemMasterModel GetById(int Id);
        IList<ItemMasterModel> GetAll();

        void Save();
    }
}
