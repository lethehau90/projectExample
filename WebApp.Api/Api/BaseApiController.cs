using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Api.Api
{
    [NotImplExceptionFilter]
    public class BaseApiController : ApiController
    {
        protected const string AreaName = "api";
    }
}
