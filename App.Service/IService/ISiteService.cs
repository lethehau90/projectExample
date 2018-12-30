using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model.Entity;
using App.Model.Models;

namespace App.Service.IService
{
    public interface ISiteService
    {
        void Add(SiteModel siteModel);
        void Update(SiteModel siteModel);
        SiteModel GetById(int Id);
        IList<SiteModel> GetAll();

        void Save();
    }
}
