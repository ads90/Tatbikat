using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tatbikat.UI.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;

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
            _baseClient.BaseAddress = new Uri(Endpoints.API_BaseUrl);
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
                if (!response.IsSuccessStatusCode)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                    await App.Current.MainPage.DisplayAlert("خطا", "Error " + response.StatusCode.ToString(), "موافق");
                    });
                }
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
                    if (!responseMessage.IsSuccessStatusCode )
                    {
                        throw new Exception($"GET Request failed, Status Code: '{responseMessage.StatusCode}");
                    }
                    string response = await responseMessage.Content?.ReadAsStringAsync();

                    TReturn obj = FromJson<TReturn>(response);

                    tcs?.TrySetResult(obj);
                }
                catch (Exception ex)
                {
                    tcs?.TrySetException(ex);
                }
            });
            return await tcs.Task;
        }
        public async Task PostAsync<TContent>(string endPoint, TContent body)
        {
            await Task.Run(async () =>
             {
                 try
                 {
                     string jsonBody = JsonConvert.SerializeObject(body);
                     StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                     HttpResponseMessage responseMessage = await _baseClient.PostAsync(endPoint, content);

                 }
                 catch (Exception ex)
                 {

                 }

             });

        }

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
