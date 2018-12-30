using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model.Entity;
using App.Model.Models;
using AutoMapper;

namespace App.Model.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ItemMasterInventory, ItemMasterInventoryModel>();
                cfg.CreateMap<ItemMaster, ItemMasterModel>();
                cfg.CreateMap<Site, SiteModel>();
            });
        }
    }
}
