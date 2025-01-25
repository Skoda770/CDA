using CDA.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using CDA.Enums;
using CDA.Factories;
using CDA.Models.Abstracts;
using CDA.Models.Downloaders;
using YoutubeDLSharp;

namespace CDA
{
    public partial class MainForm : Form
    {
        private readonly DownloaderFactory _downloaderFactory;

        public MainForm()
        {
            InitializeComponent();
            SetInitialData();
            this._downloaderFactory = new DownloaderFactory(this.listView1);
        }

        private void SetInitialData()
        {
            videoDownload.Checked = true;
            destinationCatalog.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            AbstractDownloader downloader = this._downloaderFactory.GetDownloader(this.GetDownloadType());
            await Task.Run((Func<Task>)(async () =>
                await downloader.Download(this.OriginUrl.Text, Path.GetFullPath(this.destinationCatalog.Text))));
        }

        private DownloadType GetDownloadType()
        {
            RadioButton check = (RadioButton)null;
            foreach (RadioButton button in this.downloadType.Controls.Get<RadioButton>())
            {
                if (button.Checked)
                {
                    check = button;
                    break;
                }
            }

            if (check == null)
            {
                int num = (int)MessageBox.Show("Może wybierzesz typ pobierania co?");
                throw new ArgumentException("Invalid download type");
            }

            if (check.Name == "videoDownload")
                return DownloadType.Video;
            return check.Name == "catalogArchiveOrgDownload" ? DownloadType.CatalogArchiveOrg : DownloadType.CatalogCda;
        }


        private void destinationCatalog_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.InitialDirectory = this.Controls["destinationCatalog"].Text;
            if (this.folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.Controls["destinationCatalog"].Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void NavigateToLogs_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Logger._directory);
        }

    }
}