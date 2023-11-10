using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeDLSharp;

namespace CDA.Models
{
    public class VideoDownloader : AbstractDownloader
    {
        public VideoDownloader(ListView listView) : base(listView)
        {
        }

        public override Task Download(string url, string destinationCatalog, int maxConcurrent) => Download(url, destinationCatalog, new List<Models.File>
        {
            new File
            {
                Url = url
            }
        }, maxConcurrent);

        protected override async Task Download(string url, string destinationCatalog, List<File> files, int maxConcurrent)
        {
            List<MyListViewItem<File>> listViewItems = files.Select((e, index) => new MyListViewItem<File>
            {
                Index = index,
                Item = e
            }).ToList();
            _listView.Items.AddRange(listViewItems.Select(e => e.Item.ToListViewItem()).ToArray());
            if (!Directory.Exists(destinationCatalog)) Directory.CreateDirectory(destinationCatalog);

            var progress = new Progress<DownloadProgress>(p =>
            {
                Debug.WriteLine($"Video: {p.VideoIndex}, progress: {Convert.ToInt32(p.Progress * 100)}, state: {p.State}, speed: {p.DownloadSpeed}");
                if (p.Progress != 0)
                {
                    var item = _listView.Items[0];
                    item.SubItems[1].Text = $"{Convert.ToInt32(p.Progress * 100)}%";
                    item.SubItems[2].Text = p.DownloadSpeed;
                }
            });

            var cts = new CancellationTokenSource();
            var ytdl = new YoutubeDL();

            ytdl.OutputFolder = destinationCatalog;
            await ytdl.RunVideoDownload(url, mergeFormat: YoutubeDLSharp.Options.DownloadMergeFormat.Mp4,
                                        progress: progress, ct: cts.Token);

        }
    }
}
