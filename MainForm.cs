using CDA.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using YoutubeDLSharp;

namespace CDA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetInitialData();
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
            listView1.Items.Clear();
            AbstractDownloader videoDownloader = GetDownloadType() == DownloadType.Catalog ? new CatalogDownloader(listView1) : new VideoDownloader(listView1);
            await videoDownloader.Download(OriginUrl.Text, Path.GetFullPath(destinationCatalog.Text), 4);
        }
        private DownloadType GetDownloadType()
        {
            RadioButton? check = null;
            foreach (var button in downloadType.Controls.Get<RadioButton>())
            {
                if (button.Checked)
                {
                    check = button; break;
                }
            };
            if (check is null)
            {
                MessageBox.Show("Mo¿e wybierzesz typ pobierania co?");
                throw new ArgumentException("Invalid download type");
            }
            if (check.Name == "videoDownload") return DownloadType.Video;
            return DownloadType.Catalog;
        }
        protected enum DownloadType
        {
            Video,
            Catalog
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void destinationCatalog_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = this.Controls["destinationCatalog"].Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                this.Controls["destinationCatalog"].Text = folderBrowserDialog1.SelectedPath;
        }
    }
}