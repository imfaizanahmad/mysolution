using MRM.Database.Model;
using MRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Model
{

    public class MasterCampaignViewModel
    {
        public MasterCampaignViewModel()
        {
            IndustryViewModels = new List<Industry>();
            GeographyViewModels = new List<Geography>();
            ThemeViewModels = new List<Theme>();
            SegmentViewModels = new List<Segment>();
            BusinessGroupViewModels = new List<BusinessGroup>();
            BusinessLineViewModels = new List<BusinessLine>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public int Status { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public int BusinessGroups_Id { get; set; }
        public int BusinessLines_Id { get; set; }
        public int Geographys_Id { get; set; }
        public int Industries_Id { get; set; }
        public int Segments_Id { get; set; }
        public int Themes_Id { get; set; }
    }
}
