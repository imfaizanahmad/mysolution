using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Model
{

    public class CommonEntities
    {
        //public virtual BusinessGroup BusinessGroups { get; set; }

        public List<BusinessGroup> BusinessGroups { get; set; }
        public class BusinessGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<Segment> Segments { get; set; }
        public class Segment
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public List<Geography> Geographys { get; set; }
        public class Geography
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public List<BusinessLine> BusinessLines { get; set; }
        public class BusinessLine
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<Industry> Industries { get; set; }
        public class Industry
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<Theme> Themes { get; set; }
        public class Theme
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


        public List<MasterCamPaign> MasterCamPaigns { get; set; }
        public class MasterCamPaign
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<ChildCamPaign> ChildCamPaigns { get; set; }
        public class ChildCamPaign
        {
            public int Id { get; set; }
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
        public string CampaignDescription { get; set; }
    }


    public class ChildCampaignVM : CommonEntities
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Budget { get; set; }
        public string Spend { get; set; }
        public string TargetNewPilpeline { get; set; }
        public string TargetTouchPilpeline { get; set; }
        public string ChildChampaignDescription { get; set; }

    }

    public class TacticCampaignVM : CommonEntities
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
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
            BusinessGroupsItems.Add(new CommonEntities.BusinessGroup { Id = 1, Name = "BG 1" });
            BusinessGroupsItems.Add(new CommonEntities.BusinessGroup { Id = 2, Name = "BG 2" });
            BusinessGroupsItems.Add(new CommonEntities.BusinessGroup { Id = 3, Name = "BG 3" });
            return BusinessGroupsItems;
        }

        public static List<CommonEntities.MasterCamPaign> GetMasterCamPaignItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var MasterCamPaignItems = new List<CommonEntities.MasterCamPaign>();
            MasterCamPaignItems.Add(new CommonEntities.MasterCamPaign { Id = 1, Name = "BG 1" });
            MasterCamPaignItems.Add(new CommonEntities.MasterCamPaign { Id = 2, Name = "BG 2" });
            MasterCamPaignItems.Add(new CommonEntities.MasterCamPaign { Id = 3, Name = "BG 3" });
            return MasterCamPaignItems;
        }
        public static List<CommonEntities.ChildCamPaign> GetChildCamPaignItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var ChildCamPaignItems = new List<CommonEntities.ChildCamPaign>();
            ChildCamPaignItems.Add(new CommonEntities.ChildCamPaign { Id = 1, Name = "BG 1" });
            ChildCamPaignItems.Add(new CommonEntities.ChildCamPaign { Id = 2, Name = "BG 2" });
            ChildCamPaignItems.Add(new CommonEntities.ChildCamPaign { Id = 3, Name = "BG 3" });
            return ChildCamPaignItems;
        }
        public static List<CommonEntities.BusinessLine> GetBusinessLine()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var BusinessLineItems = new List<CommonEntities.BusinessLine>();
            BusinessLineItems.Add(new CommonEntities.BusinessLine { Id = 1, Name = "BG 1" });
            BusinessLineItems.Add(new CommonEntities.BusinessLine { Id = 2, Name = "BG 2" });
            BusinessLineItems.Add(new CommonEntities.BusinessLine { Id = 3, Name = "BG 3" });
            return BusinessLineItems;
        }

        public static List<CommonEntities.Geography> GetGeographyItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var GeographyItems = new List<CommonEntities.Geography>();
            GeographyItems.Add(new CommonEntities.Geography { Id = 1, Name = "BG 1" });
            GeographyItems.Add(new CommonEntities.Geography { Id = 2, Name = "BG 2" });
            GeographyItems.Add(new CommonEntities.Geography { Id = 3, Name = "BG 3" });
            return GeographyItems;
        }
        public static List<CommonEntities.Industry> GetIndustryItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var IndustryItems = new List<CommonEntities.Industry>();
            IndustryItems.Add(new CommonEntities.Industry { Id = 1, Name = "BG 1" });
            IndustryItems.Add(new CommonEntities.Industry { Id = 2, Name = "BG 2" });
            IndustryItems.Add(new CommonEntities.Industry { Id = 3, Name = "BG 3" });
            return IndustryItems;
        }
        public static List<CommonEntities.Segment> GetSegmentItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var SegmentItems = new List<CommonEntities.Segment>();
            SegmentItems.Add(new CommonEntities.Segment { Id = 1, Name = "BG 1" });
            SegmentItems.Add(new CommonEntities.Segment { Id = 2, Name = "BG 2" });
            SegmentItems.Add(new CommonEntities.Segment { Id = 3, Name = "BG 3" });
            return SegmentItems;
        }
        public static List<CommonEntities.Theme> GetThemeItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var ThemeItems = new List<CommonEntities.Theme>();
            ThemeItems.Add(new CommonEntities.Theme { Id = 1, Name = "BG 1" });
            ThemeItems.Add(new CommonEntities.Theme { Id = 2, Name = "BG 2" });
            ThemeItems.Add(new CommonEntities.Theme { Id = 3, Name = "BG 3" });
            return ThemeItems;
        }
    }
}
