using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatbikatModels;

namespace TatbikatBLL
{
    public class AppService
    {
        private TatbikatEntities db = new TatbikatEntities();

        public List<App> GetAllApps()
        {
            return db.Apps.Where(i => i.IsVerified).ToList();
        }

        public async Task AddApp(App newApp)
        {
            newApp.IsVerified = false;
            newApp.DateAdded = DateTime.UtcNow;
            newApp.DateVerified = null;
            db.Apps.Add(newApp);
            await db.SaveChangesAsync();
        }
    }
}
