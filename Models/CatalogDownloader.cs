using Polly.Retry;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDLSharp;
using System.Diagnostics;
using System.Security.Policy;
using YoutubeDLSharp.Options;
using System.Net;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;
using System.Net.Http;

namespace CDA.Models
{
    internal class CatalogDownloader : AbstractDownloader
    {
        public CatalogDownloader(ListView listView) : base(listView)
        {
        }
        protected override async Task Download(string url, string destinationCatalog, List<File> files, int maxConcurrent)
        {
            List<MyListViewItem<File>> listViewItems = files.Select((e, index) => new MyListViewItem<File>
            {
                Index = index,
                Item = e
            }).ToList();
            _listView.Items.AddRange(listViewItems.Select(e => e.Item.ToListViewItem()).ToArray());
            if (!Directory.Exists(destinationCatalog)) Directory.CreateDirectory(destinationCatalog);
            List<Task> tasks = new();

            foreach (var file in listViewItems)
                tasks.Add(DownloadVideo(file, destinationCatalog));

            await Task.WhenAll(tasks);
        }
        private string[] GetUrls(string url)
        {
            return GetVideoUrlsFromWebPage(url);
        }
        public override Task Download(string url, string destinationCatalog, int maxConcurrent)
        {
            var files = GetUrls(url).Select(e => new Models.File { Url = e }).ToList();
            return Download(url, destinationCatalog, files, maxConcurrent);
        }

        protected async Task DownloadVideo(MyListViewItem<File> file, string destinationCatalog)
        {
            var progress = new Progress<DownloadProgress>(p =>
            {
                Debug.WriteLine($"Video: {p.VideoIndex}, progress: {Convert.ToInt32(p.Progress * 100)}, state: {p.State}, speed: {p.DownloadSpeed}");
                if (p.Progress != 0)
                {
                    var item = _listView.Items[file.Index];
                    item.SubItems[1].Text = $"{Convert.ToInt32(p.Progress * 100)}%";
                    item.SubItems[2].Text = p.DownloadSpeed;
                }
            });
            var cts = new CancellationTokenSource();
            var ytdl = new YoutubeDL();

            ytdl.OutputFolder = destinationCatalog;
            await ytdl.RunVideoDownload(file.Item.Url, mergeFormat: DownloadMergeFormat.Mp4,
                                        progress: progress, ct: cts.Token);

        }

        private string[] GetVideoUrlsFromWebPage(string url)
        {
            List<string> videoUrls = new List<string>();

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--silent");
            options.AddArgument("--log-level=3");
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            IWebDriver driver = new ChromeDriver(service, options);
            try
            {
                driver.Navigate().GoToUrl(url);
                string pageSource = driver.PageSource;
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(pageSource);
                List<string> links = new List<string>();
                HtmlNodeCollection divsWithAttribute = doc.DocumentNode.SelectNodes($"//div[@data-foldery_id]");

                if (divsWithAttribute != null)
                {
                    foreach (HtmlNode div in divsWithAttribute)
                    {
                        // Znajdź wszystkie linki wewnątrz tego <div>.
                        //
                        string pattern = @"(video/)";
                        var regex = new Regex(pattern);
                        return div
                            .SelectNodes(".//a[@href]")
                            .Select(e => e.GetAttributeValue("href", string.Empty))
                            .ToList()
                            .Where(e => regex.IsMatch(e))
                            .Distinct()
                            .Select(e => $"https://www.cda.pl{e}")
                            .ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów, np. wyświetlenie komunikatu użytkownikowi
                MessageBox.Show("Wystąpił błąd: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }

            return videoUrls.ToArray();
        }
    }
}
