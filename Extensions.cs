using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA
{
    public static class Extensions
    {
        public static List<T> Get<T>(this Control.ControlCollection collection)
        {
            var result = new List<T>();
            foreach (var control in collection) 
            {
                if (control.GetType() == typeof(T)) result.Add((T)control);
            }

            return result;
        }
    }
}
