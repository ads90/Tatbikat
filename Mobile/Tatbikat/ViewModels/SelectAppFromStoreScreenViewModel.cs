using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Models.Enums;
using Tatbikat.Operations;
using Tatbikat.UI.Interfaces;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class SelectAppFromStoreScreenViewModel : ViewModelsBase, ICallbackEnabledScreen<TatbikatApp>
    {
        PlatformType _platformType;
        TaskCompletionSource<TatbikatApp> _pageTcs;
        public Command<string> SearchForAppCommand { get; set; }
        public SelectAppFromStoreScreenViewModel(PlatformType platformType)
        {
            SearchForAppCommand = new Command<string>(SearchForAppCommandFunction);
            _pageTcs = new TaskCompletionSource<TatbikatApp>();
            _platformType = platformType;
        }

        private string _appSearchText;
        public string AppSearchText
        {
            get { return _appSearchText; }
            set { RefreshUIProperty(ref _appSearchText, value); }
        }
        private List<TatbikatApp> _appSearchResult = new List<TatbikatApp>();
        public List<TatbikatApp> AppSearchResult
        {
            get { return _appSearchResult; }
            set { RefreshUIProperty(ref _appSearchResult, value); }
        }
        private TatbikatApp _selectedApp;
        public TatbikatApp SelectedApp
        {
            get { return _selectedApp; }
            set
            {
                RefreshUIProperty(ref _selectedApp, value);
                OnSelectedApp();
            }
        }

        private void OnSelectedApp()
        {
            if (SelectedApp == null)
            {
                return;
            }
            _pageTcs?.TrySetResult((TatbikatApp)SelectedApp);
        }

        private async void SearchForAppCommandFunction(string appname)
        {
            //if (string.IsNullOrWhiteSpace(appname))
            //{
            //    return;
            //}
            IsLoading = true;
            if(_platformType== PlatformType.iOS)
            {
                //SearchForiOSApp(appname);
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync("https://play.google.com/store/search?q=optio&c=apps");

                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(html);
                var appsList = htmlDocument.DocumentNode.Descendants("div").Where(node => node.Attributes.Contains("class")&&node.Attributes["class"].Value.Contains("id-card-list")).First().SelectNodes("div");
                AppSearchResult = new List<TatbikatApp>();
                foreach(var app in appsList)
                {
                    var appimagetext = "http://"+(app.Descendants("img").Where(img => img.Attributes.Contains("class") && img.Attributes["class"].Value.Contains("cover-image")).First().Attributes["src"]).Value.TrimStart('/', '/');
                    var appnametext = (app.Descendants("div").Where(txt => txt.Attributes.Contains("class") && txt.Attributes["class"].Value.Contains("details"))).First().Descendants("a").Where(d=>d.Attributes["class"].Value.Contains("title")).First().Attributes["title"].Value;
                    var appidtext = (app.Descendants("div").Where(txt => txt.Attributes.Contains("class") && txt.Attributes["class"].Value.Contains("cover"))).First().Descendants("a").First().SelectNodes("span").Where(s=>s.Attributes["class"].Value.Contains("preview-overlay-container")).First().Attributes["data-docid"].Value;

                    var appdescriptiontexToExeclude = (app.Descendants("div").Where(txt => txt.Attributes.Contains("class") && txt.Attributes["class"].Value.Contains("details"))).First().Descendants("div").Where(x => x.Attributes["class"].Value.Contains("description")).First().SelectNodes("span").Where(s => s.Attributes["class"].Value.Contains("paragraph-end")).First();
                    var appdescriptiontex2 = (app.Descendants("div").Where(txt => txt.Attributes.Contains("class") && txt.Attributes["class"].Value.Contains("details"))).First().Descendants("div").Where(x => x.Attributes["class"].Value.Contains("description")).First().SelectSingleNode(".//span[@class='paragraph-end']").OuterHtml.Trim();//.InnerHtml.Trim();//.(appdescriptiontexToExeclude);//.SelectNodes("snap"));//.`[]appdescriptiontexToExeclude);//.SelectSingleNode("description");//.Descendants("a").Where(d => d.Attributes["class"].Value.Contains("title")).First().Attributes["title"].Value;
                   // var appdescriptiontex = (app.Descendants("div").Where(txt => txt.Attributes.Contains("class") && txt.Attributes["class"].Value.Contains("details"))).First().Descendants("div").Where(x => x.Attributes["class"].Value.Contains("description")).First().RemoveChild(appdescriptiontex2);

                    AppSearchResult.Add(new TatbikatApp() {Name=appnametext, ImageSource = appimagetext });

                }

                //var optio =appsList[0].
                //var doc = Task.Factory.StartNew(()=>)
            }
            else
            { 
                SearchForAndroidApp(appname);
            }
            IsLoading = false;
        }

        private void SearchForAndroidApp(string appname)
        {
             
        }

        private async void SearchForiOSApp(string appname)
        {
            var searchParams = $"/search?term={appname}&country=sa&entity=software";
            var result = await Connector.Current.GetAppsFromiOSStore(searchParams);
            List<TatbikatApp> castedResult = (List<TatbikatApp>)result;
            AppSearchResult = castedResult; 
        }
        public override void NavigateBackRequested()
        {
            _pageTcs?.TrySetResult(null);
        }
        public Task<TatbikatApp> Wait()
        {
            return _pageTcs.Task;
        }
    }
}
