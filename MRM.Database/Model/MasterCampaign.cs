using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
    public class MasterCampaignVM
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string CompanyDescription { get; set; }
        public Int64 MetaDataId { get; set; }
        public Int64 BusinessGroupLookupId { get; set; }

        public string MasterCampaigns { get; set; }
        public string MasterCampaingNaming { get; set; }

        public List<BusinessGroup> BusinessGroupsItems { get; set; }
        public class BusinessGroup
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }


        public List<Segment> SegmentItems { get; set; }
        public class Segment
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public List<Geography> GeographyItems { get; set; }
        public class Geography
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public List<BusinessLine> BusinessLineItems { get; set; }
        public class BusinessLine
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }


        public List<Industry> IndustryItems { get; set; }
        public class Industry
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public List<Theme> ThemeItems { get; set; }
        public class Theme
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

    }
}
