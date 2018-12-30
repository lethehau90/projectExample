using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using App.Model.Models;
using App.Service.IService;
using App.Service.Service;

namespace WebApp.Api.Api
{
    [RoutePrefix(EndPoint)]
    public class ItemMasterController : BaseApiController
    {
        private const string EndPoint = AreaName + "/item-master";
        private const string AddEndPoint = "add";
        private const string UpdatedEndPoint = "update";
        private const string GetbyIdEndPoint = "getById/{Id}";
        private const string GetallEndPoint = "getall";
        private const string SaveImageEndPoint = "saveImage";

        public IItemMasterService _itemMasterService;

        public ItemMasterController(IItemMasterService itemMasterService)
        {
            _itemMasterService = itemMasterService;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <returns>ItemMasterModel</returns>
        [Route(GetbyIdEndPoint)]
        [HttpGet]
        public ItemMasterModel GetById(int Id)
        {
            var model = _itemMasterService.GetById(Id);
            return model;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns>IList<ItemMasterModel></returns>
        [Route(GetallEndPoint)]
        [HttpGet]
        public IList<ItemMasterModel> GetAll()
        {
            var model = _itemMasterService.GetAll();
            return model;
        }

        /// <summary>
        ///  Add Item Master
        /// </summary>
        /// <param name="itemMasterModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(AddEndPoint)]
        public void Add( ItemMasterModel itemMasterModel)
        {
            _itemMasterService.Add(itemMasterModel);
            _itemMasterService.Save();
        }

        /// <summary>
        ///  Update Item Master
        /// </summary>
        /// <param name="itemMasterModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(UpdatedEndPoint)]
        public void Update(ItemMasterModel itemMasterModel)
        {   
            _itemMasterService.Update(itemMasterModel);
            _itemMasterService.Save();
        }


        /// <summary>
        ///  save Image
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(SaveImageEndPoint)]
        public HttpResponseMessage SaveImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {

                    int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                        dict.Add("error", message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                    }
                    else if (postedFile.ContentLength > MaxContentLength)
                    {
                        var message = string.Format("Please Upload a file upto 1 mb.");
                        dict.Add("error", message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                    }
                    else
                    {
                        string directory = string.Empty;
                        directory = "/UploadedFiles/";
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                        }

                        string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), postedFile.FileName);
                        //Userimage myfolder name where i want to save my image
                        postedFile.SaveAs(path);
                        return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, postedFile.FileName));
                    }
                }

                var message1 = string.Format("Image Updated Successfully.");
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
            }
            var res = string.Format("Please Upload a image.");
            dict.Add("error", res);
            return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        }
    }
}
