using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
