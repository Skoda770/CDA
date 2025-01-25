using CDA.Models.Abstracts;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Polly.Bulkhead;
using Polly.Wrap;

namespace CDA.Models.Downloaders;

  public class CatalogArchiveOrgDownloader(
    ListView listView,
    AsyncBulkheadPolicy bulkheadPolicy,
    AsyncPolicyWrap policy) : AbstractCatalogDownloader(listView, bulkheadPolicy, policy)
  {
    private static List<string> GetUrlsFromWebsite(IWebDriver driver, string url)
    {
      driver.Navigate().GoToUrl(url);
      string pageSource = driver.PageSource;
      HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
      htmlDocument.LoadHtml(pageSource);
      List<string> stringList = new List<string>();
      htmlDocument.DocumentNode.SelectNodes("//input[@class='js-play8-playlist']");
      HtmlNodeCollection source = htmlDocument.DocumentNode.SelectNodes("//input[@class='js-play8-playlist']");
      HtmlNode htmlNode = source != null ? source.FirstOrDefault<HtmlNode>() : (HtmlNode) null;
      return htmlNode == null || !htmlNode.Attributes.Any<HtmlAttribute>((Func<HtmlAttribute, bool>) (e => e.Name == "value")) ? new List<string>() : JsonConvert.DeserializeObject<List<CatalogArchiveOrgDownloader.Class1>>(htmlNode.Attributes.First<HtmlAttribute>((Func<HtmlAttribute, bool>) (e => e.Name == "value")).Value.Replace("&quot;", "\"")).Select<CatalogArchiveOrgDownloader.Class1, string>((Func<CatalogArchiveOrgDownloader.Class1, string>) (e => ((IEnumerable<CatalogArchiveOrgDownloader.Source>) e.sources).FirstOrDefault<CatalogArchiveOrgDownloader.Source>()?.file)).Distinct<string>().Select<string, string>((Func<string, string>) (e => "https://archive.org" + e)).ToList<string>();
    }

    public override async Task<IEnumerable<string>> GetUrls(string url)
    {
      List<string> videoUrls = new List<string>();
      ChromeOptions options = new ChromeOptions();
      options.AddArgument("--headless");
      options.AddArgument("--disable-gpu");
      options.AddArgument("--silent");
      options.AddArgument("--log-level=3");
      ChromeDriverService service = ChromeDriverService.CreateDefaultService();
      service.HideCommandPromptWindow = true;
      await Task.Run((Action) (() =>
      {
        IWebDriver driver = (IWebDriver) new ChromeDriver(service, options);
        try
        {
          videoUrls = CatalogArchiveOrgDownloader.GetUrlsFromWebsite(driver, url);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Wystąpił błąd: " + ex.Message);
        }
        finally
        {
          driver.Quit();
        }
      }));
      return (IEnumerable<string>) videoUrls.ToList<string>();
    }

    protected class Rootobject
    {
      public CatalogArchiveOrgDownloader.Class1[] Property1 { get; set; }
    }

    protected class Class1
    {
      public string title { get; set; }

      public string orig { get; set; }

      public string image { get; set; }

      public object duration { get; set; }

      public CatalogArchiveOrgDownloader.Source[] sources { get; set; }

      public CatalogArchiveOrgDownloader.Track[] tracks { get; set; }
    }

    protected class Source
    {
      public string file { get; set; }

      public string type { get; set; }

      public string height { get; set; }

      public string width { get; set; }

      public string label { get; set; }
    }

    protected class Track
    {
      public string kind { get; set; }

      public string file { get; set; }

      public string label { get; set; }
    }
  }