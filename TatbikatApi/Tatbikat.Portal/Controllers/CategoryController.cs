using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TatbikatBLL;
using TatbikatModels;

namespace Tatbikat.Portal.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService catSer = new CategoryService();
        // GET: Category
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var cats = catSer.GetAllCategory(pageNumber, pageSize);
            return View(cats);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model, HttpPostedFileBase image)
        {
            catSer.AddCategory(model, image);
            return RedirectToAction("Index");
        }

    }
}