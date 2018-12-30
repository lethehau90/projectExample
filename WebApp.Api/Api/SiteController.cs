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
    public class SiteController : BaseApiController
    {
        private const string EndPoint = AreaName + "/site";
        private const string AddEndPoint = "add";
        private const string UpdatedEndPoint = "update";
        private const string GetbyIdEndPoint = "getById/{Id}";
        private const string GetAllEndPoint = "getall";

        public ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Site</returns>
        [Route(GetbyIdEndPoint)]
        [HttpGet]
        public SiteModel GetById(int Id)
        {
            var model = _siteService.GetById(Id);
            return model;
        }


        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>SiteModel</returns>
        [Route(GetAllEndPoint)]
        [HttpGet]
        public IList<SiteModel> GetAll()
        {
            var model = _siteService.GetAll();
            return model;
        }


        /// <summary>
        ///  Add Item Master
        /// </summary>
        /// <param name="siteModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(AddEndPoint)]
        public HttpResponseMessage Add(SiteModel siteModel)
        {
            HttpRequestMessage request = null;
            if (ModelState.IsValid)
            {
                _siteService.Add(siteModel);
                _siteService.Save();
                return request.CreateResponse(HttpStatusCode.OK, siteModel);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /// <summary>
        ///  Update Item Master
        /// </summary>
        /// <param name="siteModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(UpdatedEndPoint)]
        public HttpResponseMessage Update(SiteModel siteModel)
        {
            HttpRequestMessage request = null;
            if (ModelState.IsValid)
            {
                _siteService.Update(siteModel);
                _siteService.Save();
                return request.CreateResponse(HttpStatusCode.OK, siteModel);
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


    }
}
