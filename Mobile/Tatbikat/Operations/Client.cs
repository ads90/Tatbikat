using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tatbikat.UI.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Tatbikat.Operations
{
    public class Client : DelegatingHandler
    {
        IInternetStatusService _internetStatus;
        HttpClient _baseClient;
        public Client()
        {
            InnerHandler = new HttpClientHandler();
            _internetStatus = DependencyService.Get<IInternetStatusService>();
           _baseClient = new HttpClient(this);
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                if (!_internetStatus.IsConnected())
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.DisplayAlert(":(", "لايوجد اتصال بالانترنت", "موافق");
                    });
                }

                    response = await base.SendAsync(request, cancellationToken); 
                return response;
            }
            
            catch (Exception nce)
            {
                throw nce;
            }
        }

        public async Task<TReturn> GetAsync<TReturn>(string endPoint, string @params)
        {
            TaskCompletionSource<TReturn> tcs = new TaskCompletionSource<TReturn>();
          
            await Task.Run(async () =>
            {
                try
                {
                    HttpResponseMessage responseMessage = await _baseClient.GetAsync(endPoint + @params);
                    string response = await responseMessage.Content?.ReadAsStringAsync();
                   
                    TReturn obj = FromJson<TReturn>(response);

                    tcs?.TrySetResult(obj);
                }
                catch (Exception ex)
                {
                   // tcs?.TrySetException(ex);
                }
            });
            return await tcs.Task;
        }
        public async Task<TReturn> GetAsync<TReturn>(string endPoint)
        {
            TaskCompletionSource<TReturn> tcs = new TaskCompletionSource<TReturn>();

            await Task.Run(async () =>
            {
                try
                {
                    HttpResponseMessage responseMessage = await _baseClient.GetAsync(endPoint);
                    string response = await responseMessage.Content?.ReadAsStringAsync();

                    TReturn obj = FromJson<TReturn>(response);

                    tcs?.TrySetResult(obj);
                }
                catch (Exception ex)
                {
                    // tcs?.TrySetException(ex);
                }
            });
            return await tcs.Task;
        }
        //    public async Task<TReturn> PostAsync<TReturn, TContent>(string endPoint, TContent body)
        //    {
        //        TaskCompletionSource<TReturn> tcs = new TaskCompletionSource<TReturn>();

        //        await Task.Run(async () =>
        //        {
        //            try
        //            {
        //                TReturn res = await InternalPostAsync<TReturn, TContent>(endPoint, body, new Dictionary<string, object>());
        //                tcs?.TrySetResult(res);
        //            }
        //            catch (Exception ex)
        //            {
        //                ex.Trace(TraceOptions.Local, endPoint);
        //                tcs?.TrySetException(ex);
        //            }
        //        });
        //        return await tcs.Task;

        //    }
        //}
        private T FromJson<T>(string response)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
