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
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SiteService(ISiteRepository itemMasterRepository, IUnitOfWork unitOfWork)
        {
            this._siteRepository = itemMasterRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(SiteModel itemMasterModel)
        {
            Site newSite = new Site();
            newSite.UpdateSite(itemMasterModel);
            _siteRepository.Add(newSite);
        }

        public IList<SiteModel> GetAll()
        {
            var query = _siteRepository.GetAll();
            return Mapper.Map <IList<SiteModel>>(query);
        }

        public SiteModel GetById(int Id)
        {
            var query = _siteRepository.GetSingleById(Id);
            if (query == null) return null;
            return Mapper.Map<SiteModel>(query);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

        public void Update(SiteModel itemMasterModel)
        {
            var model = _siteRepository.GetSingleById(itemMasterModel.Id);
            model.UpdateSite(itemMasterModel);
        }
    }
}
