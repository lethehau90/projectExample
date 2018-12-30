using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Model.Models;
using App.Service.IService;
using App.Service.Service;

namespace WebApp.Api.Api
{
    [RoutePrefix(EndPoint)]
    public class ItemMasterInventoryController : BaseApiController
    {
        private const string EndPoint = AreaName + "/item-master-inventory";
        private const string AddEndPoint = "add";
        private const string UpdatedEndPoint = "update";
        private const string GetMutiEndPoint = "getmuti/{Id}";

        public IItemMasterInventoryService _itemMasterInventoryService;

        public ItemMasterInventoryController(IItemMasterInventoryService itemMasterInventoryService)
        {
            _itemMasterInventoryService = itemMasterInventoryService;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ItemMasterInventoryModel</returns>
        [Route(GetMutiEndPoint)]
        [HttpGet]
        public IList<listMasterJoinSiteModel> GetMuti(int Id)
        {
            var model = _itemMasterInventoryService.GetMuti(Id).ToList();
            return model;
        }

        /// <summary>
        ///  Add Item Master
        /// </summary>
        /// <param name="itemMasterInventoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(AddEndPoint)]
        public HttpResponseMessage Add(ItemMasterInventoryModel itemMasterInventoryModel)
        {
            HttpRequestMessage request = null;
            if (ModelState.IsValid)
            {
                _itemMasterInventoryService.Add(itemMasterInventoryModel);
                _itemMasterInventoryService.Save();
                return request.CreateResponse(HttpStatusCode.OK, itemMasterInventoryModel);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /// <summary>
        ///  Update Item Master
        /// </summary>
        /// <param name="itemMasterInventoryModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(UpdatedEndPoint)]
        public HttpResponseMessage Update(ItemMasterInventoryModel itemMasterInventoryModel)
        {
            HttpRequestMessage request = null;
            if (ModelState.IsValid)
            {
                _itemMasterInventoryService.Update(itemMasterInventoryModel);
                _itemMasterInventoryService.Save();
                return request.CreateResponse(HttpStatusCode.OK, itemMasterInventoryModel);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
