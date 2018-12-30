using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Infrastructure;
using App.Data.Repositories.IRepository;
using App.Model.Entity;
using App.Model.Mapping.Infrastructure.Extensions;
using App.Model.Models;
using App.Service.IService;
using AutoMapper;

namespace App.Service.Service
{
    public class ItemMasterService : IItemMasterService
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemMasterService(IItemMasterRepository itemMasterRepository, IUnitOfWork unitOfWork)
        {
            this._itemMasterRepository = itemMasterRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(ItemMasterModel itemMasterModel)
        {
            ItemMaster newItemMaster = new ItemMaster();
            newItemMaster.UpdateItemMaster(itemMasterModel);
             _itemMasterRepository.Add(newItemMaster);
        }

        public IList<ItemMasterModel> GetAll()
        {
            var query = _itemMasterRepository.GetAll();
            return Mapper.Map<IList<ItemMasterModel>>(query);
        }

        public ItemMasterModel GetById(int Id)
        {
            var query = _itemMasterRepository.GetSingleById(Id);
            if (query == null) return null;
            return Mapper.Map<ItemMasterModel>(query);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ItemMasterModel itemMasterModel)
        {
            var model = _itemMasterRepository.GetSingleById(itemMasterModel.Id);
            model.UpdateItemMaster(itemMasterModel);
        }
    }
}
