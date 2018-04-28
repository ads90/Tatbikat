using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tatbikat.Models;

namespace Tatbikat.Operations
{
    public partial class Connector
    {
        public async Task<List<TatbikatApp>> GetApps()
        {
            try
            {
                ///search?term=awnak&country=sa&entity=software
                List<TatbikatApp> apiResult = await Client.GetAsync<List<TatbikatApp>>(Endpoints.Apps.GetAllApps);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                List<Category> apiResult = await Client.GetAsync<List<Category>>(Endpoints.Apps.GetAllCategories);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task PostNewApp(TatbikatApp @params)
        {
            try
            {
                ///search?term=awnak&country=sa&entity=software
                await Client.PostAsync<TatbikatApp>(Endpoints.Apps.PostNewApp, @params);
                // return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
