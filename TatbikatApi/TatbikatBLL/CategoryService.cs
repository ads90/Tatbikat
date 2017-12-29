using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TatbikatModels;

namespace TatbikatBLL
{
    public class CategoryService
    {
        private TatbikatEntities db = new TatbikatEntities();

        public List<Category> GetAllCategory()
        {
            return db.Categories.ToList();
        }

        public IPagedList<Category> GetAllCategory(int page = 0, int pageSize = 10)
        {
            IQueryable<Category> catList = db.Categories.OrderBy(i => i.Id).AsQueryable();
            return catList.ToPagedList(page, pageSize);
        }

        public void AddCategory(Category model, HttpPostedFileBase image)
        {
            db.Categories.Add(model);
            db.SaveChanges();
        }
    }
}
