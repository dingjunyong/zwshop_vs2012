using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ZwShop.WebApi.Controller
{
    public class CategoriesController : ApiController
    {
        public HttpResponseMessage Get(string id)
        {
            List<string> result=new List<string>();
            result.Add("11111");
            result.Add("22222");
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        public HttpResponseMessage Delete(string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}