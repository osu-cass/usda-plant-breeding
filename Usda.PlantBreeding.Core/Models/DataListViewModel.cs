using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class DataListViewModel 
    {
        /// <summary>
        /// Key is the object id
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Value is the object name to search on
        /// </summary>
        public string value { get; set; }
       
    }

    public class DataListViewModelComparer : IEqualityComparer<DataListViewModel>
    {

        public string GetKey(DataListViewModel obj)
        {
            return obj.key + obj.value;
        }
        public bool Equals(DataListViewModel x, DataListViewModel y)
        {

            return GetKey(x) == GetKey(y);
        }

        public int GetHashCode(DataListViewModel obj)
        {
            return GetKey(obj).GetHashCode();
        }

    }
}
