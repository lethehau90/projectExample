using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model.Entity;
using App.Model.Models;

namespace App.Model.Mapping.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateItemMasterInventory(this ItemMasterInventory itemMasterInventory, ItemMasterInventoryModel itemMasterInventoryVm)
        {
            itemMasterInventory.Id = itemMasterInventoryVm.Id;
            itemMasterInventory.IMISiteID = itemMasterInventoryVm.IMISiteID;
            itemMasterInventory.IMIQtyOnHand = itemMasterInventoryVm.IMIQtyOnHand;
            itemMasterInventory.IMIQtyAllocated = itemMasterInventoryVm.IMIQtyAllocated;
            itemMasterInventory.ItemMasterID = itemMasterInventoryVm.ItemMasterID;
        }

        public static void UpdateItemMaster(this ItemMaster itemMaster, ItemMasterModel itemMasterVm)
        {
            itemMaster.Id = itemMasterVm.Id;
            itemMaster.IMPack = itemMasterVm.IMPack;
            itemMaster.IMDescription = itemMasterVm.IMDescription;
            itemMaster.IMImageData = itemMasterVm.IMImageData;
            itemMaster.IMIsHazardousMaterial = itemMasterVm.IMIsHazardousMaterial;
            itemMaster.IMExpirationDate = itemMasterVm.IMExpirationDate;
            itemMaster.IMUnitPrice = itemMasterVm.IMUnitPrice;
            itemMaster.IMWidth = itemMasterVm.IMWidth;
            itemMaster.IMLength = itemMasterVm.IMLength;
            itemMaster.IMHeight = itemMasterVm.IMHeight;
            itemMaster.IMIsPrePack = itemMasterVm.IMIsPrePack;
            itemMaster.IMPrePackStyle = itemMasterVm.IMPrePackStyle;
            itemMaster.IMCostCenterCode = itemMasterVm.IMCostCenterCode;
        }

        public static void UpdateSite(this Site site, SiteModel siteVm)
        {
            site.Id = siteVm.Id;
            site.Name = siteVm.Name;
        }
    }
}
