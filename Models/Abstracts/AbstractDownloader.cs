using System.Runtime.CompilerServices;
using CDA.Factories;
using Polly.Bulkhead;
using Polly.Wrap;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace CDA.Models.Abstracts;


  public abstract class AbstractDownloader
  {
    protected readonly ListView _listView;
    protected readonly AsyncBulkheadPolicy _bulkheadPolicy;
    protected readonly AsyncPolicyWrap _policy;

    public AbstractDownloader(
      ListView listView,
      AsyncBulkheadPolicy bulkheadPolicy,
      AsyncPolicyWrap policy)
    {
      this._listView = listView;
      this._bulkheadPolicy = bulkheadPolicy;
      this._policy = policy;
    }

    protected virtual async Task Download(string destinationCatalog, List<CDA.Models.File> files)
    {
      int initialIndex = this._listView.Items.Count;
      List<MyListViewItem<CDA.Models.File>> listViewItems = files.Select<CDA.Models.File, MyListViewItem<CDA.Models.File>>((Func<CDA.Models.File, int, MyListViewItem<CDA.Models.File>>) ((e, index) => new MyListViewItem<CDA.Models.File>()
      {
        Index = initialIndex + index,
        Item = e
      })).ToList<MyListViewItem<CDA.Models.File>>();
      this._listView.Invoke((Action) (() => this._listView.Items.AddRange(listViewItems.Select<MyListViewItem<CDA.Models.File>, ListViewItem>((Func<MyListViewItem<CDA.Models.File>, ListViewItem>) (e => e.Item.ToListViewItem())).ToArray<ListViewItem>())));
      if (!Directory.Exists(destinationCatalog))
        Directory.CreateDirectory(destinationCatalog);
      await Task.WhenAll(listViewItems.Select<MyListViewItem<CDA.Models.File>, Task>((Func<MyListViewItem<CDA.Models.File>, Task>) (file =>
      {
        int count = 0;
        return this._policy.ExecuteAsync((Func<Task>) (async () => await this.DownloadVideo(file, destinationCatalog, count++)));
      })));
    }

    private async Task DownloadVideo(
      MyListViewItem<CDA.Models.File> file,
      string destinationCatalog,
      int count)
    {
      Progress<DownloadProgress> progress1 = new Progress<DownloadProgress>((Action<DownloadProgress>) (p =>
      {
        if ((double) p.Progress != 0.0)
          this._listView.Invoke((Action) (() =>
          {
            ListViewItem listViewItem = this._listView.Items[file.Index];
            ListViewItem.ListViewSubItem subItem = listViewItem.SubItems[1];
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
            interpolatedStringHandler.AppendFormatted<int>(Convert.ToInt32(p.Progress * 100f));
            interpolatedStringHandler.AppendLiteral("%");
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            subItem.Text = stringAndClear;
            listViewItem.SubItems[2].Text = (double) p.Progress == 1.0 ? "" : p.DownloadSpeed;
          }));
        if (p.State != DownloadState.Error)
          return;
        this._listView.Invoke((Action) (() =>
        {
          ListViewItem listViewItem = this._listView.Items[file.Index];
          if (DownloaderFactory._retryAttemps <= count)
            return;
          ListViewItem.ListViewSubItem subItem = listViewItem.SubItems[1];
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
          interpolatedStringHandler.AppendLiteral("Błąd. Próba ");
          interpolatedStringHandler.AppendFormatted<int>(count + 1);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          subItem.Text = stringAndClear;
        }));
      }));
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      YoutubeDL youtubeDl = new YoutubeDL();
      youtubeDl.OutputFolder = destinationCatalog;
      youtubeDl.OverwriteFiles = true;
      youtubeDl.OutputFileTemplate = "%(title)s.%(ext)s";
      string url = file.Item.Url;
      IProgress<DownloadProgress> progress2 = (IProgress<DownloadProgress>) progress1;
      CancellationToken token = cancellationTokenSource.Token;
      IProgress<DownloadProgress> progress3 = progress2;
      RunResult<string> runResult = await youtubeDl.RunVideoDownload(url, mergeFormat: DownloadMergeFormat.Mp4, ct: token, progress: progress3);
      if (!runResult.Success)
      {
        this._listView.Invoke((Action) (() =>
        {
          if (DownloaderFactory._retryAttemps != count)
            return;
          this._listView.Items[file.Index].SubItems[1].Text = "Niepowodzenie";
        }));
        throw new ArgumentException(string.Join(", ", runResult.ErrorOutput));
      }
    }

    public abstract Task Download(string url, string destinationCatalog);
  }