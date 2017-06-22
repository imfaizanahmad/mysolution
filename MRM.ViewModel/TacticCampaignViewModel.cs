using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;

namespace MRM.ViewModel
{

    public class TacticCampaignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string Year { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public int ChildCampaign_Id { get; set; }

        public string ReachR1Goal { get; set; }
        public string ReachR1Low { get; set; }
        public string ReachR1High { get; set; }
        public string ReachR11Goal { get; set; }
        public string ReachR12Low { get; set; }
        public string ReachR13High { get; set; }
        public string ResponseR1Goal { get; set; }
        public string ResponseR1Low { get; set; }
        public string ResponseR1High { get; set; }
        public string ResponseR21Goal { get; set; }
        public string ResponseR22Low { get; set; }
        public string ResponseR23High { get; set; }


        public string EfficiencyE1Goal { get; set; }
        public string EfficiencyE1Low { get; set; }
        public string EfficiencyE1High { get; set; }

        public string EfficiencyE11Goal { get; set; }
        public string EfficiencyE12Low { get; set; }
        public string EfficiencyE13High { get; set; }

        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public IEnumerable<ChildCampaign> ChildCampaignViewModels { get; set; }
        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }

        public int[] Vendor_Id { get; set; }
    }
}
