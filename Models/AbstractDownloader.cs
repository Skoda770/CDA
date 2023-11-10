using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Models
{
    public abstract class AbstractDownloader
    {
        protected readonly ListView _listView;

        public AbstractDownloader(ListView listView)
        {
            _listView = listView;
        }
        protected abstract Task Download(string url, string destinationCatalog, List<File> files, int maxConcurrent);
        public abstract Task Download(string url, string destinationCatalog, int maxConcurrent);
    }
}
