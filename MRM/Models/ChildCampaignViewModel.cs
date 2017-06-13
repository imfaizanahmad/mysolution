using MRM.Database.Model;
using MRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Model
{

    public class ChildCampaignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string Budget { get; set; }
        public string Spend { get; set; }
        public string TargetNewPilpeline { get; set; }
        public string TargetTouchPilpeline { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public IEnumerable<MasterCampaign> MasterViewModels { get; set; }
    }
}
