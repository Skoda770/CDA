using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CDA.Models.Abstracts;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Polly.Bulkhead;
using Polly.Wrap;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace CDA.Models.Downloaders;


  public class CatalogCDADownloader(
    ListView listView,
    AsyncBulkheadPolicy bulkheadPolicy,
    AsyncPolicyWrap policy) : AbstractCatalogDownloader(listView, bulkheadPolicy, policy)
  {
    private static List<string> GetUrlsFromCDA(IWebDriver driver, string url)
    {
      driver.Navigate().GoToUrl(url);
      string pageSource = driver.PageSource;
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(pageSource);
      List<string> stringList = new List<string>();
      HtmlNodeCollection divsWithAttribute = htmlDocument.DocumentNode.SelectNodes("//div[@data-foldery_id]");
      if (divsWithAttribute != null)
      {
        using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>) divsWithAttribute).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            HtmlNode current = enumerator.Current;
            Regex regex = new Regex("(video/)");
            return current.SelectNodes(".//a[@href]").Select<HtmlNode, string>((Func<HtmlNode, string>) (e => e.GetAttributeValue("href", string.Empty))).ToList<string>().Where<string>((Func<string, bool>) (e => regex.IsMatch(e))).Distinct<string>().Select<string, string>((Func<string, string>) (e => "https://www.cda.pl" + e)).ToList<string>();
          }
        }
      }
      return new List<string>();
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
          int catalogCounter = 1;
          while (true)
          {
            string str1 = url;
            string str2;
            if (catalogCounter <= 1)
            {
              str2 = "";
            }
            else
            {
              DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
              interpolatedStringHandler.AppendLiteral("/");
              interpolatedStringHandler.AppendFormatted<int>(catalogCounter);
              str2 = interpolatedStringHandler.ToStringAndClear();
            }
            string urlWithVideos = str1 + str2;
            List<string> result = CatalogCDADownloader.GetUrlsFromCDA(driver, urlWithVideos);
            if (result.Count != 0)
            {
              videoUrls.AddRange((IEnumerable<string>) result);
              ++catalogCounter;
            }
            else
              break;
          }
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
  }