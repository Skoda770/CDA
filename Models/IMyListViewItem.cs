using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Models
{
    public interface IMyListViewItem
    {
        ListViewItem ToListViewItem();
    }
}
