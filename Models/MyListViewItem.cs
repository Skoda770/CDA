using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Models
{
    public class MyListViewItem<T> where T : class
    {
        public int Index { get; set; }
        public T Item { get; set; }
    }
}
