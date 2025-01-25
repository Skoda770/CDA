using Polly.Bulkhead;
using Polly.Wrap;

namespace CDA.Models.Abstracts;


public abstract class AbstractCatalogDownloader : AbstractDownloader
{
    public AbstractCatalogDownloader(
        ListView listView,
        AsyncBulkheadPolicy bulkheadPolicy,
        AsyncPolicyWrap policy)
        : base(listView, bulkheadPolicy, policy)
    {
    }

    public override async Task Download(string url, string destinationCatalog)
    {
        IEnumerable<string> urls = await this.GetUrls(url);
        await Download(destinationCatalog, urls.Select<string, File>((Func<string, File>) (e => new File()
        {
            Url = e
        })).ToList<File>());
    }

    public abstract Task<IEnumerable<string>> GetUrls(string url);
}