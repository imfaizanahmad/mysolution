using MRM.Database.Model;
using MRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Model
{

    public class TacticCampaignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string Year { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Status { get; set; }

        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public IEnumerable<ChildCampaign> ChildCampaignViewModels { get; set; }
    }
}
