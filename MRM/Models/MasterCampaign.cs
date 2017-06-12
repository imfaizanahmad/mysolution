using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Model
{

    public class CommonEntities
    {
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


        public List<MasterCamPaign> MasterCamPaignItems { get; set; }
        public class MasterCamPaign
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public List<ChildCamPaign> ChildCamPaignItems { get; set; }
        public class ChildCamPaign
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public string Status { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }


    }

    public class MasterCampaignVM : CommonEntities
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string CompanyDescription { get; set; }
        public Int64 MetaDataId { get; set; }
        public Int64 BusinessGroupLookupId { get; set; }

        public string MasterCampaigns { get; set; }
        public string MasterCampaingName { get; set; }
    
    }


    public class ChildCampaignVM : CommonEntities
    {
        public string Budget { get; set; }
        public string Spend { get; set; }

        public string TargetNewPilpeline { get; set; }
        public string TargetTouchPilpeline { get; set; }
        public string ChildChampaignName { get; set; }

        public string ChildChampaignNaming { get; set; }

        public string ChildChampaignDescription { get; set; }

    }

    public class TacticCampaignVM : CommonEntities
    {
        public string Budget { get; set; }
        public string Spend { get; set; }

        public string TargetNewPilpeline { get; set; }
        public string TargetTouchPilpeline { get; set; }
        public string ChildChampaignName { get; set; }

        public string ChildChampaignNaming { get; set; }

        public string ChildChampaignDescription { get; set; }

        public Int64 MarketID { get; set; }
        public string VendorName { get; set; }
        public string TacticName { get; set; }
        public string Year { get; set; }
        public string TacticDescription { get; set; }
    }



    //Dropdown dummy data
    public class DropdownDummy
    {
        public static List<CommonEntities.BusinessGroup> GetBusinessGroupsItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var BusinessGroupsItems = new List<CommonEntities.BusinessGroup>();
            BusinessGroupsItems.Add(new CommonEntities.BusinessGroup { ID = 1, Name = "BG 1" });
            BusinessGroupsItems.Add(new CommonEntities.BusinessGroup { ID = 2, Name = "BG 2" });
            BusinessGroupsItems.Add(new CommonEntities.BusinessGroup { ID = 3, Name = "BG 3" });
            return BusinessGroupsItems;
        }

        public static List<CommonEntities.MasterCamPaign> GetMasterCamPaignItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var MasterCamPaignItems = new List<CommonEntities.MasterCamPaign>();
            MasterCamPaignItems.Add(new CommonEntities.MasterCamPaign { ID = 1, Name = "BG 1" });
            MasterCamPaignItems.Add(new CommonEntities.MasterCamPaign { ID = 2, Name = "BG 2" });
            MasterCamPaignItems.Add(new CommonEntities.MasterCamPaign { ID = 3, Name = "BG 3" });
            return MasterCamPaignItems;
        }
        public static List<CommonEntities.ChildCamPaign> GetChildCamPaignItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var ChildCamPaignItems = new List<CommonEntities.ChildCamPaign>();
            ChildCamPaignItems.Add(new CommonEntities.ChildCamPaign { ID = 1, Name = "BG 1" });
            ChildCamPaignItems.Add(new CommonEntities.ChildCamPaign { ID = 2, Name = "BG 2" });
            ChildCamPaignItems.Add(new CommonEntities.ChildCamPaign { ID = 3, Name = "BG 3" });
            return ChildCamPaignItems;
        }
        public static List<CommonEntities.BusinessLine> GetBusinessLine()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var BusinessLineItems = new List<CommonEntities.BusinessLine>();
            BusinessLineItems.Add(new CommonEntities.BusinessLine { ID = 1, Name = "BG 1" });
            BusinessLineItems.Add(new CommonEntities.BusinessLine { ID = 2, Name = "BG 2" });
            BusinessLineItems.Add(new CommonEntities.BusinessLine { ID = 3, Name = "BG 3" });
            return BusinessLineItems;
        }

        public static List<CommonEntities.Geography> GetGeographyItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var GeographyItems = new List<CommonEntities.Geography>();
            GeographyItems.Add(new CommonEntities.Geography { ID = 1, Name = "BG 1" });
            GeographyItems.Add(new CommonEntities.Geography { ID = 2, Name = "BG 2" });
            GeographyItems.Add(new CommonEntities.Geography { ID = 3, Name = "BG 3" });
            return GeographyItems;
        }
        public static List<CommonEntities.Industry> GetIndustryItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var IndustryItems = new List<CommonEntities.Industry>();
            IndustryItems.Add(new CommonEntities.Industry { ID = 1, Name = "BG 1" });
            IndustryItems.Add(new CommonEntities.Industry { ID = 2, Name = "BG 2" });
            IndustryItems.Add(new CommonEntities.Industry { ID = 3, Name = "BG 3" });
            return IndustryItems;
        }
        public static List<CommonEntities.Segment> GetSegmentItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var SegmentItems = new List<CommonEntities.Segment>();
            SegmentItems.Add(new CommonEntities.Segment { ID = 1, Name = "BG 1" });
            SegmentItems.Add(new CommonEntities.Segment { ID = 2, Name = "BG 2" });
            SegmentItems.Add(new CommonEntities.Segment { ID = 3, Name = "BG 3" });
            return SegmentItems;
        }
        public static List<CommonEntities.Theme> GetThemeItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var ThemeItems = new List<CommonEntities.Theme>();
            ThemeItems.Add(new CommonEntities.Theme { ID = 1, Name = "BG 1" });
            ThemeItems.Add(new CommonEntities.Theme { ID = 2, Name = "BG 2" });
            ThemeItems.Add(new CommonEntities.Theme { ID = 3, Name = "BG 3" });
            return ThemeItems;
        }
    }
}
