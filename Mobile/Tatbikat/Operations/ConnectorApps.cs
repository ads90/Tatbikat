﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tatbikat.Models;

namespace Tatbikat.Operations
{
    public partial class Connector
    {
        public async Task<List<TatbikatApp>> GetApps(string @params)
        {
            try
            {
                ///search?term=awnak&country=sa&entity=software
                List<TatbikatApp> apiResult = await Client.GetAsync<List<TatbikatApp>>(Endpoints.Apps.GetAllApps, @params);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<TatbikatApp>> PostNewApp(string @params)
        {
            try
            {
                ///search?term=awnak&country=sa&entity=software
                List<TatbikatApp> apiResult = await Client.GetAsync<List<TatbikatApp>>(Endpoints.Apps.PostNewApp, @params);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
