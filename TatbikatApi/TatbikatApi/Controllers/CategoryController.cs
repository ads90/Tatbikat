using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TatbikatBLL;
using TatbikatModels;

namespace TatbikatApi.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryService catSer = new CategoryService();

        [HttpGet]
        public IHttpActionResult GetAllCategory()
        {
            List<Category> allCats = catSer.GetAllCategory();
            return Ok(new { allCats });
        }
    }
}
