using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRM.Models
{
    public class SegmentHierarchy
    {
        public SegmentHierarchy()
        {
            SegmentHierarchyLst = new List<SegmentHierarchyList>();
        }

        public bool IsSuccess { get; set; }
        public IList<SegmentHierarchyList> SegmentHierarchyLst { get; set; }

        public class SegmentHierarchyList
        {
            public int SegmentId { get; set; }
            public string SegmentName { get; set; }
            public int IndustryId { get; set; }
            public string IndustryName { get; set; }
        }
    }
}