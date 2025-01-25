using CDA.Enums;
using CDA.Models;
using CDA.Models.Abstracts;
using CDA.Models.Downloaders;
using Polly;
using Polly.Bulkhead;
using Polly.Wrap;

namespace CDA.Factories;


public class DownloaderFactory
{
    private readonly IEnumerable<AbstractDownloader> _downloaders;
    private readonly AsyncBulkheadPolicy _bulkheadPolicy;
    private readonly AsyncPolicyWrap _policy;

    public static int _retryAttemps => 3;

    public static int _waitSeconds => 20;

    public DownloaderFactory(ListView listView)
    {
        this._bulkheadPolicy = Policy.BulkheadAsync(6, int.MaxValue);
        this._policy = Policy.Handle<Exception>().WaitAndRetryAsync(DownloaderFactory._retryAttemps, (Func<int, TimeSpan>) (_ => TimeSpan.FromSeconds((double) DownloaderFactory._waitSeconds))).WrapAsync((IAsyncPolicy) this._bulkheadPolicy);
        this._downloaders = (IEnumerable<AbstractDownloader>) new List<AbstractDownloader>()
        {
            (AbstractDownloader) new CatalogCDADownloader(listView, this._bulkheadPolicy, this._policy),
            (AbstractDownloader) new CatalogArchiveOrgDownloader(listView, this._bulkheadPolicy, this._policy),
            (AbstractDownloader) new VideoDownloader(listView, this._bulkheadPolicy, this._policy)
        };
    }

    private AbstractDownloader GetByType(System.Type type)
    {
        return this._downloaders.FirstOrDefault<AbstractDownloader>((Func<AbstractDownloader, bool>) (e => e.GetType() == type)) ?? throw new ArgumentException("Missing downloader");
    }

    public AbstractDownloader GetDownloader(DownloadType downloadType)
    {
        switch (downloadType)
        {
            case DownloadType.Video:
                return this.GetByType(typeof (VideoDownloader));
            case DownloadType.CatalogCda:
                return this.GetByType(typeof (CatalogCDADownloader));
            case DownloadType.CatalogArchiveOrg:
                return this.GetByType(typeof (CatalogArchiveOrgDownloader));
            default:
                throw new ArgumentException("DownloadType - invalid enum value");
        }
    }
}