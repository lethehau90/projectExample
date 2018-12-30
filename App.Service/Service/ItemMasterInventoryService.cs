using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Infrastructure;
using App.Data.Repositories.IRepository;
using App.Data.Repositories.Repository;
using App.Model.Entity;
using App.Model.Mapping.Infrastructure.Extensions;
using App.Model.Models;
using App.Service.IService;
using AutoMapper;

namespace App.Service.Service
{
    public class ItemMasterInventoryService : IItemMasterInventoryService
    {
        private readonly IItemMasterInventoryRepository _itemMasterinventoryRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemMasterInventoryService(IItemMasterInventoryRepository itemMasterInventoryRepository, IUnitOfWork unitOfWork, ISiteRepository siteRepository)
        {
            this._itemMasterinventoryRepository = itemMasterInventoryRepository;
            this._unitOfWork = unitOfWork;
            this._siteRepository = siteRepository;
        }
        public void Add(ItemMasterInventoryModel itemMasterInventoryModel)
        {
            ItemMasterInventory newEntity = new ItemMasterInventory();
            newEntity.UpdateItemMasterInventory(itemMasterInventoryModel);
            this._itemMasterinventoryRepository.Add(newEntity);
        }

        public IEnumerable<listMasterJoinSiteModel> GetMuti(int Id)
        {
            var query = (from T1 in _itemMasterinventoryRepository.GetMulti(x => x.IMISiteID == Id).ToList()
                         join T2 in _siteRepository.GetAll() on T1.IMISiteID equals T2.Id
                          select new listMasterJoinSiteModel
                          {
                              Id = T1.Id,
                              IMIQtyAllocated = T1.IMIQtyAllocated,
                              IMIQtyOnHand = T1.IMIQtyOnHand,
                              IMISiteID = T1.IMISiteID,
                              ItemMasterID = T1.ItemMasterID,
                              NameSite = T2.Name
                          });
            return query;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ItemMasterInventoryModel itemMasterInventoryModel)
        {
            var queryEntity = _itemMasterinventoryRepository.GetSingleById(itemMasterInventoryModel.Id);
            queryEntity.UpdateItemMasterInventory(itemMasterInventoryModel);
            _itemMasterinventoryRepository.Update(queryEntity);
        }
    }
}
