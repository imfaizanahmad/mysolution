using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;

namespace MRM.ViewModel
{

    public class MasterCampaignViewModel
    {
        public MasterCampaignViewModel()
        {
            BusinessGroupViewModels = new[] { new BusinessGroup() };
            SegmentViewModels = (new[] { new Segment() });
            BusinessLineViewModels = (new[] { new BusinessLine() });
            ThemeViewModels = (new[] { new Theme() });
            GeographyViewModels = (new[] { new Geography() });
            IndustryViewModels = (new[] { new Industry() });
            StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            EndDate = DateTime.Now.ToString("MM/dd/yyyy");
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string Status { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }
        //public int[] SelectedThemes_Id {get; set;}
    }
}
