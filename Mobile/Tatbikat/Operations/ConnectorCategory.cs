using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tatbikat.Models;

namespace Tatbikat.Operations
{
    public partial class Connector
    {
        public async Task<InternalIOSApp> GetAppsFromiOSStore(string @params)
        {
            try
            {
                ///search?term=awnak&country=sa&entity=software
                InternalIOSApp apiResult = await Client.GetAsync<InternalIOSApp>(Endpoints.IOSStoreSearch, @params);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
