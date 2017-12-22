using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tatbikat.Models;

namespace Tatbikat.Operations
{
    public partial class Connector
    {
        public async Task<List<TatbikatApp>> GetAppsFromiOSStore()
        {
            try
            {
                List<TatbikatApp> apiResult = await Client.GetAsync<List<TatbikatApp>>(Endpoints.IOSStoreSearch);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
