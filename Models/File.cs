using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Models
{
    public class File : IMyListViewItem
    {
        public string Url { get; set; }
        public string DownloadSpeed { get; set; }
        public string Progress { get; set; }
        public string Label { get; set; }

        public ListViewItem ToListViewItem()
        {
            return new ListViewItem(new string[]
            {
                Url,
                Progress,
                DownloadSpeed
            });
        }
    }
}
