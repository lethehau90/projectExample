﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Infrastructure;
using App.Data.Repositories.IRepository;
using App.Model.Entity;

namespace App.Data.Repositories.Repository
{
    public class ItemMasterRepository : RepositoryBase<ItemMaster>, IItemMasterRepository
    {
        public ItemMasterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}