using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Net;  
using System.Net.Http;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace WebApp.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, context.Exception.Message);
        }
    }


    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }

        public string ContentType { get; set; } = @"text/plain";

        public HttpStatusCodeException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(int statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }

    }
}
