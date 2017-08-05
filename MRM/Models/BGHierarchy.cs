using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRM.Models
{
    public class BGHierarchy
    {
        public BGHierarchy()
        {
            BGHierarchyLst = new List<BGHierarchyList>();
        }

        public bool IsSuccess { get; set; }
        public IList<BGHierarchyList> BGHierarchyLst { get; set; }

        public class BGHierarchyList
        {
            public int BGId { get; set; }
            public string BGName { get; set; }
            public int BLId { get; set; }
            public string BLName { get; set; }
        }
    }
}