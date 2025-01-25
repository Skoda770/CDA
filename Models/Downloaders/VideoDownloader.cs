using CDA.Models.Abstracts;
using Polly.Bulkhead;
using Polly.Wrap;

namespace CDA.Models.Downloaders;


public class VideoDownloader(
    ListView listView,
    AsyncBulkheadPolicy bulkheadPolicy,
    AsyncPolicyWrap policy) : AbstractDownloader(listView, bulkheadPolicy, policy)
{
    public override Task Download(string url, string destinationCatalog)
    {
        return this.Download(destinationCatalog, new List<File>()
        {
            new File() { Url = url }
        });
    }
}